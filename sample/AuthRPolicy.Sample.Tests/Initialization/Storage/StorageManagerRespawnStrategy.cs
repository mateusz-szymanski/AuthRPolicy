using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public class StorageManagerRespawnStrategy : IStorageManagerStrategy
    {
        private readonly IDbContextFactory<SampleDbContext> _dbContextFactory;
        private Respawner? _respawner;

        public int Order => 2;

        public StorageManagerRespawnStrategy(IDbContextFactory<SampleDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task InitializeStorage()
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            _respawner = await Respawner.CreateAsync(
                dbContext.Database.GetDbConnection().ConnectionString,
                new RespawnerOptions()
                {
                    DbAdapter = DbAdapter.SqlServer
                }
            );
        }

        public async Task CleanupStorage()
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            await (_respawner?.ResetAsync(dbContext.Database.GetDbConnection().ConnectionString) ?? Task.CompletedTask);
        }
    }
}
