using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
