using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public static class Extensions
    {
        public static IServiceCollection AddStorageManager(
            this IServiceCollection services,
            StorageManagerStrategy storageManagerStrategy)
        {
            services.AddSingleton<IStorageManager, StorageManager>();

            if (StorageManagerStrategy.Respawn == storageManagerStrategy)
                services.AddSingleton<IStorageManagerStrategy, StorageManagerRespawnStrategy>();
            else
                services.AddSingleton<IStorageManagerStrategy, StorageManagerRecreateStrategy>();

            return services;
        }

        public static ConfigurationBuilder AddConnectionString(
            this ConfigurationBuilder configurationBuilder,
            ConnectionStringProvider connectionStringProvider)
        {
            var connectionString = connectionStringProvider.GetConnectionString();

            configurationBuilder.AddInMemoryCollection(
                new Dictionary<string, string> {
                    { "ConnectionStrings:Sample", connectionString }
                }
            );

            return configurationBuilder;
        }
    }
}
