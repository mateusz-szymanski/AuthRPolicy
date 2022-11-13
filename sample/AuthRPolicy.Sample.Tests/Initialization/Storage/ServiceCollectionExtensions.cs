using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using AuthRPolicy.Sample.Tests.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureStorage(
            this IServiceCollection services,
            ConnectionStringProvider connectionStringProvider,
            StorageManagerStrategy storageManagerStrategy)
        {
            services.AddSingleton<IStorageManager, StorageManager>();

            if (StorageManagerStrategy.Respawn == storageManagerStrategy)
                services.AddSingleton<IStorageManagerStrategy, StorageManagerRespawnStrategy>();
            else
                services.AddSingleton<IStorageManagerStrategy, StorageManagerRecreateStrategy>();

            var connectionString = connectionStringProvider.GetConnectionString();
            var dbContextOptions = new DbContextOptionsBuilder<SampleDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            services.ReplaceService<DbContextOptions>(dbContextOptions);
            services.ReplaceService(dbContextOptions);

            return services;
        }
    }
}
