using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public class StorageManagerRecreateStrategy : IStorageManagerStrategy
    {
        private readonly IDbContextFactory<SampleDbContext> _dbContextFactory;

        public int Order => 1;

        public StorageManagerRecreateStrategy(IDbContextFactory<SampleDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task InitializeStorage()
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            await dbContext.Database.EnsureCreatedAsync();
        }

        public async Task CleanupStorage()
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            await dbContext.Database.EnsureDeletedAsync();
        }
    }
}
