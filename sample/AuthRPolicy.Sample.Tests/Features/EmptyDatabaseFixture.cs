using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using AuthRPolicy.Sample.Tests.Initialization.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.Sample.Tests.Features
{
    public class EmptyDatabaseFixture : IAsyncLifetime
    {
        public ConnectionStringProvider ConnectionStringProvider { get; private set; }

        private readonly ServiceProvider _serviceProvider;

        public EmptyDatabaseFixture()
        {
            ConnectionStringProvider = new();

            var configuration = new ConfigurationBuilder()
                .Build();

            var services = new ServiceCollection();

            services
                .AddEntityFramework(configuration)
                .ConfigureStorage(ConnectionStringProvider, StorageManagerStrategy.Recreate);

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
