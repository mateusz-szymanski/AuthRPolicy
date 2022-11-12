using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization
{
    public interface IStorageManager
    {
        Task InitializeStorage();
    }
}