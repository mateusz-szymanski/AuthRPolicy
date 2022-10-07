using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Authorization.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorization<TRoleProvider, TPermissionProvider>(
            this IServiceCollection services,
            Assembly[] assemblies)
        {


            return services;
        }
    }
}
