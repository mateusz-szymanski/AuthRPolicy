using AuthRPolicy.Sample.Tests.Mediator;
using AuthRPolicy.Sample.Tests.UserMocking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization
{
    public class Application : IApplication
    {
        private readonly ServiceProvider _services;
        public IServiceProvider Services => _services;

        public MediatorRunner Mediator { get; }
        public UserSwitcher UserSwitcher { get; }

        public Application(ServiceProvider services)
        {
            _services = services;

            UserSwitcher = _services.GetRequiredService<UserSwitcher>();
            Mediator = new(_services);
        }

        public async Task Initialize()
        {
            var storageManager = Services.GetRequiredService<IStorageManager>();

            await storageManager.InitializeStorage();
        }

        public ValueTask DisposeAsync()
        {
            return _services.DisposeAsync();
        }
    }
}
