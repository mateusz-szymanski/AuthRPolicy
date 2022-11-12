using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public interface IStorageManagerStrategy
    {
        int Order { get; }
        Task InitializeStorage();
        Task CleanupStorage();
    }
}