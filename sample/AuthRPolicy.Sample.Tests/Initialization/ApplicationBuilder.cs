using AuthRPolicy.MediatRExtensions.Services;
using AuthRPolicy.Sample.IoC;
using AuthRPolicy.Sample.Tests.Extensions;
using AuthRPolicy.Sample.Tests.Initialization.Storage;
using AuthRPolicy.Sample.Tests.UserMocking;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization
{
    public class ApplicationBuilder
    {
        private StorageManagerStrategy _storageManagerStrategy;
        private StorageConfigurationProvider _storageConfigurationProvider;

        public ApplicationBuilder()
        {
            _storageConfigurationProvider = new();
            _storageManagerStrategy = StorageManagerStrategy.Recreate;
        }

        public ApplicationBuilder WithExistingDatabase(StorageConfigurationProvider storageConfigurationProvider)
        {
            _storageConfigurationProvider = storageConfigurationProvider;
            _storageManagerStrategy = StorageManagerStrategy.Respawn;

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

            services.ConfigureDatabase(_storageConfigurationProvider, _storageManagerStrategy);

            var serviceProvider = services.BuildServiceProvider();

            var application = new Application(serviceProvider);
            await application.Initialize();

            return application;
        }
    }
}
