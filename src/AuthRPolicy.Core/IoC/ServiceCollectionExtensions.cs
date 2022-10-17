using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Roles;
using AuthRPolicy.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AuthRPolicy.Core.IoC
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers per scope RoleProvider, PermissionProvider, AuthorizationService and all access policy checkers in given assemblies.
        /// </summary>
        /// <typeparam name="TRoleProvider"></typeparam>
        /// <typeparam name="TPermissionProvider"></typeparam>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorization<TRoleProvider>(
            this IServiceCollection services,
            params Assembly[] assemblies)
            where TRoleProvider : class, IRoleProvider
        {
            services.AddScoped<IRoleProvider, TRoleProvider>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddScoped<IAccessPolicyChecker<EmptyAccessPolicy>, EmptyAccessPolicyChecker>();

            services.RegisterAccessPolicyCheckersFromAssembly(assemblies);

            return services;
        }

        public static IServiceCollection AddAuthorization(
            this IServiceCollection services,
            Action<AuthorizationOptions> authorizationOptionsBuilder)
        {
            var authorizationOptions = new AuthorizationOptions();
            authorizationOptionsBuilder(authorizationOptions);
            var roleProvider = authorizationOptions.RolesBuilder.Build();

            services.AddSingleton(roleProvider);
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddScoped<IAccessPolicyChecker<EmptyAccessPolicy>, EmptyAccessPolicyChecker>();

            services.RegisterAccessPolicyCheckersFromAssembly(authorizationOptions.Assemblies);

            return services;
        }

        private static IServiceCollection RegisterAccessPolicyCheckersFromAssembly(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.GetTypes());
            var accessPolicyTypes = allTypes.Where(t => t.IsAssignableTo(typeof(IAccessPolicy)));
            var accessPolicyCheckerInterfaceTypes = accessPolicyTypes.Select(apt => typeof(IAccessPolicyChecker<>).MakeGenericType(apt));

            foreach (var accessPolicyCheckerInterfaceType in accessPolicyCheckerInterfaceTypes)
            {
                var implementationTypes = allTypes
                    .Where(type => DoesTypeSupportInterface(type, accessPolicyCheckerInterfaceType) && !type.IsAbstract);

                if (implementationTypes.Count() != 1)
                    throw new Exception($"There must exactly one implementation for {accessPolicyCheckerInterfaceType.Name}"); // TODO: Custom exception

                var implementationType = implementationTypes.Single();
                var serviceDescriptor = new ServiceDescriptor(accessPolicyCheckerInterfaceType, implementationType, ServiceLifetime.Scoped);

                services.Add(serviceDescriptor);
            }

            return services;
        }

        private static bool DoesTypeSupportInterface(Type type, Type interfaceType)
        {
            if (interfaceType.IsAssignableFrom(type))
                return true;
            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                return true;

            return false;
        }
    }
}
