using System;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public interface IStorageManager : IAsyncDisposable
    {
        Task InitializeStorage();
    }
}