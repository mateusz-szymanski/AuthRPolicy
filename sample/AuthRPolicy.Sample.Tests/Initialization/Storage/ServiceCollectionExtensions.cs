using AuthRPolicy.Sample.Tests.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabase(
            this IServiceCollection services,
            StorageConfigurationProvider storageConfigurationProvider,
            StorageManagerStrategy storageManagerStrategy)
        {
            services.AddSingleton<IStorageManager, StorageManager>();

            if (StorageManagerStrategy.Respawn == storageManagerStrategy)
                services.AddSingleton<IStorageManagerStrategy, StorageManagerRespawnStrategy>();
            else
                services.AddSingleton<IStorageManagerStrategy, StorageManagerRecreateStrategy>();

            var dbContextOptions = storageConfigurationProvider.GetDbOptions();

            services.ReplaceService<DbContextOptions>(dbContextOptions);
            services.ReplaceService(dbContextOptions);

            return services;
        }
    }
}
