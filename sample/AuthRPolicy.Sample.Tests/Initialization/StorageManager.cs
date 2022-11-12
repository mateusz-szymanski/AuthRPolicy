using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization
{
    public class StorageManager : IStorageManager, IAsyncDisposable
    {
        private readonly IDbContextFactory<SampleDbContext> _dbContextFactory;
        private readonly bool _createDatabase;

        private Respawner? _respawner;

        public StorageManager(IDbContextFactory<SampleDbContext> dbContextFactory, bool createDatabase)
        {
            _dbContextFactory = dbContextFactory;
            _createDatabase = createDatabase;
        }

        public async Task InitializeStorage()
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            if (_createDatabase)
                await dbContext.Database.EnsureCreatedAsync();

            _respawner = await Respawner.CreateAsync(
                dbContext.Database.GetDbConnection().ConnectionString,
                new RespawnerOptions()
                {
                    DbAdapter = DbAdapter.SqlServer
                }
            );
        }

        public async ValueTask DisposeAsync()
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            await (_respawner?.ResetAsync(dbContext.Database.GetDbConnection().ConnectionString) ?? Task.CompletedTask);

            if (_createDatabase)
                await dbContext.Database.EnsureDeletedAsync();
        }
    }
}
