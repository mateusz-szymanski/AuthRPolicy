using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public interface IStorageManager
    {
        Task InitializeStorage();
        Task CleanupStorage();
    }
}