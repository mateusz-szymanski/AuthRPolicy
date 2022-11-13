using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public class StorageManager : IStorageManager
    {
        private readonly IEnumerable<IStorageManagerStrategy> _storageManagerStrategies;

        public StorageManager(IEnumerable<IStorageManagerStrategy> storageManagerStrategies)
        {
            _storageManagerStrategies = storageManagerStrategies.OrderBy(s => s.Order).ToList();
        }

        public async Task InitializeStorage()
        {
            foreach (var strategy in _storageManagerStrategies)
            {
                await strategy.InitializeStorage();
            }
        }

        public async Task CleanupStorage()
        {
            foreach (var strategy in _storageManagerStrategies)
            {
                await strategy.CleanupStorage();
            }
        }
    }
}
