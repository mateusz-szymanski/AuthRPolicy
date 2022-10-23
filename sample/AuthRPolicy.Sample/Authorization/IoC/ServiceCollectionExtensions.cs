using AuthRPolicy.Core.IoC;
using AuthRPolicy.MediatRExtensions.IoC;
using AuthRPolicy.Sample.Authorization.Permissions;
using Microsoft.Extensions.DependencyInjection;

namespace AuthRPolicy.Sample.Authorization.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
        {
            services
                .AddAuthorization(options =>
                {
                    options.Assemblies = new[] { typeof(ServiceCollectionExtensions).Assembly };

                    var rolesBuilder = options.RolesBuilder;

                    rolesBuilder.AddRole(Roles.DocumentCreator, CreateDocument.Default);
                    rolesBuilder.AddRole(Roles.DocumentReviewer, ListDocuments.AsReviwer);
                    rolesBuilder.AddRole(Roles.Admin, ListDocuments.AsAdmin);

                    rolesBuilder.ConnectPermissions(CreateDocument.Default, ListDocuments.AsOwner);
                })
                .AddAuthorizationBehavior();

            return services;
        }

        public static IServiceCollection AddApplicationAuthorization2(this IServiceCollection services)
        {
            services.AddAuthorization<RoleProvider>(typeof(ServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}
