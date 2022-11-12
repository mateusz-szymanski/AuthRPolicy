using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization
{
    public class StorageManager : IStorageManager, IAsyncDisposable
    {
        private readonly IDbContextFactory<SampleDbContext> _dbContextFactory;

        public StorageManager(IDbContextFactory<SampleDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task InitializeStorage()
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            await dbContext.Database.EnsureCreatedAsync();
        }

        public async ValueTask DisposeAsync()
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            await dbContext.Database.EnsureDeletedAsync();
        }
    }
}
