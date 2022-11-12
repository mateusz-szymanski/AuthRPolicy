using AuthRPolicy.MediatRExtensions.Services;
using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using AuthRPolicy.Sample.IoC;
using AuthRPolicy.Sample.Tests.Extensions;
using AuthRPolicy.Sample.Tests.UserMocking;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization
{
    public class ApplicationBuilder
    {
        private bool _createDatabase;
        private StorageConfigurationProvider _storageConfigurationProvider;

        public ApplicationBuilder()
        {
            _storageConfigurationProvider = new();
            _createDatabase = true;
        }

        public ApplicationBuilder WithExistingDatabase(StorageConfigurationProvider storageConfigurationProvider)
        {
            _storageConfigurationProvider = storageConfigurationProvider;
            _createDatabase = false;
            return this;
        }

        public async Task<IApplication> Build()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .Build();

            services
                .AddNullLogger()
                .AddApplicationServices(configuration)
                .AddMediatR(typeof(ApplicationBuilder).Assembly);

            services
                .AddSingleton<UserSwitcher, UserSwitcher>()
                .ReplaceService<ICurrentUserService>(sp => sp.GetRequiredService<UserSwitcher>(), ServiceLifetime.Singleton);

            services.AddSingleton<IStorageManager, StorageManager>(
                sp => new StorageManager(sp.GetRequiredService<IDbContextFactory<SampleDbContext>>(), _createDatabase)
            );

            ConfigureDatabase(services, _storageConfigurationProvider);

            var serviceProvider = services.BuildServiceProvider();

            var application = new Application(serviceProvider);
            await application.Initialize();

            return application;
        }

        private static void ConfigureDatabase(IServiceCollection services, StorageConfigurationProvider storageConfigurationProvider)
        {
            var dbContextOptions = storageConfigurationProvider.GetDbOptions();

            services.ReplaceService<DbContextOptions>(dbContextOptions);
            services.ReplaceService(dbContextOptions);
        }
    }
}
