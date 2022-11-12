using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using AuthRPolicy.Sample.Tests.Initialization;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.Sample.Tests.Features
{
    public class EmptyDatabaseFixture : IAsyncLifetime
    {
        public StorageConfigurationProvider StorageConfigurationProvider { get; private set; }

        public EmptyDatabaseFixture()
        {
            StorageConfigurationProvider = new StorageConfigurationProvider();
        }

        public async Task InitializeAsync()
        {
            using var dbContext = new SampleDbContext(StorageConfigurationProvider.GetDbOptions());

            await dbContext.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            using var dbContext = new SampleDbContext(StorageConfigurationProvider.GetDbOptions());

            await dbContext.Database.EnsureDeletedAsync();
        }
    }
}
