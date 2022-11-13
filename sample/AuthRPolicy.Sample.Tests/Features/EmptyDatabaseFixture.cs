using AuthRPolicy.Sample.IoC;
using AuthRPolicy.Sample.Tests.Initialization.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.Sample.Tests.Features
{
    public class EmptyDatabaseFixture : IAsyncLifetime
    {
        public StorageConfigurationProvider StorageConfigurationProvider { get; private set; }

        private readonly ServiceProvider _serviceProvider;

        public EmptyDatabaseFixture()
        {
            StorageConfigurationProvider = new();

            var configuration = new ConfigurationBuilder()
                .Build();

            var services = new ServiceCollection();

            services
                .AddApplicationServices(configuration)
                .ConfigureDatabase(StorageConfigurationProvider, StorageManagerStrategy.Recreate);

            _serviceProvider = services.BuildServiceProvider();
        }

        public async Task InitializeAsync()
        {
            var storageManager = _serviceProvider.GetRequiredService<IStorageManager>();

            await storageManager.InitializeStorage();
        }

        public async Task DisposeAsync()
        {
            await _serviceProvider.DisposeAsync();
        }
    }
}
