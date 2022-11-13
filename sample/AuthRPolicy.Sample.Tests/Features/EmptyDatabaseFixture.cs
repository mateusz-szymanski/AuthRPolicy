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
                .AddConnectionString(ConnectionStringProvider)
                .Build();

            var services = new ServiceCollection();

            services
                .AddSingleton<IConfiguration>(configuration)
                .AddEntityFramework()
                .AddStorageManager(StorageManagerStrategy.Recreate);

            _serviceProvider = services.BuildServiceProvider();
        }

        public async Task InitializeAsync()
        {
            var storageManager = _serviceProvider.GetRequiredService<IStorageManager>();
            await storageManager.InitializeStorage();
        }

        public async Task DisposeAsync()
        {
            var storageManager = _serviceProvider.GetRequiredService<IStorageManager>();
            await storageManager.CleanupStorage();

            await _serviceProvider.DisposeAsync();
        }
    }
}
