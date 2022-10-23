using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.IoC.Exceptions;
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
        /// Registers per scope given RoleProvider, AuthorizationService and all access policy checkers in given assemblies.
        /// </summary>
        /// <typeparam name="TRoleProvider">Custom IRoleProvider implementation.</typeparam>
        /// <param name="services">Service collection.</param>
        /// <param name="assemblies">Assemblies with AccesPolicyChecker implementations.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddAuthorization<TRoleProvider>(this IServiceCollection services, params Assembly[] assemblies)
            where TRoleProvider : class, IRoleProvider
        {
            services.AddScoped<IRoleProvider, TRoleProvider>();
            services.AddBasicServices(assemblies);

            return services;
        }

        /// <summary>
        /// Registers per scope AuthorizationService and all access policy checkers in given assemblies.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="authorizationOptionsBuilder">Options.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddAuthorization(this IServiceCollection services, Action<AuthorizationOptions> authorizationOptionsBuilder)
        {
            var authorizationOptions = new AuthorizationOptions();
            authorizationOptionsBuilder(authorizationOptions);
            var roleProvider = authorizationOptions.RolesBuilder.Build();

            services.AddSingleton(roleProvider);
            services.AddBasicServices(authorizationOptions.Assemblies);

            return services;
        }

        private static IServiceCollection AddBasicServices(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IAccessPolicyChecker<EmptyAccessPolicy>, EmptyAccessPolicyChecker>();
            services.RegisterAccessPolicyCheckersFromAssembly(assemblies);

            return services;
        }

        private static IServiceCollection RegisterAccessPolicyCheckersFromAssembly(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.GetTypes());
            var accessPolicyTypes = allTypes.Where(t => t.IsAssignableTo(typeof(IAccessPolicy)));
            var accessPolicyCheckerInterfaceTypes = accessPolicyTypes.Select(apt => typeof(IAccessPolicyChecker<>).MakeGenericType(apt));

            foreach (var accessPolicyCheckerInterfaceType in accessPolicyCheckerInterfaceTypes)
            {
                var implementationTypes = allTypes
                    .Where(type => !type.IsAbstract && DoesTypeSupportInterface(type, accessPolicyCheckerInterfaceType));

                if (implementationTypes.Count() != 1)
                    throw IncorrectAccessPolicyCheckerDefinitionException.New(accessPolicyCheckerInterfaceType);

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

            return false;
        }
    }
}
