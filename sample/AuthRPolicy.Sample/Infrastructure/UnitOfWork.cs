using AuthRPolicy.Sample.Domain;
using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleDbContext _sampleDbContext;

        public UnitOfWork(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }

        public async Task SaveChanges()
        {
            await _sampleDbContext.SaveChangesAsync();
        }
    }
}
