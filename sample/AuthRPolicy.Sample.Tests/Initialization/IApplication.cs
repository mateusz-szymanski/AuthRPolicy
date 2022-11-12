using AuthRPolicy.Sample.Tests.Mediator;
using AuthRPolicy.Sample.Tests.UserMocking;
using System;

namespace AuthRPolicy.Sample.Tests.Initialization
{
    public interface IApplication : IAsyncDisposable
    {
        MediatorRunner Mediator { get; }
        IServiceProvider Services { get; }
        UserSwitcher UserSwitcher { get; }
    }
}