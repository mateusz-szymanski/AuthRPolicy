using AuthRPolicy.Core;
using AuthRPolicy.MediatRExtensions.IoC;
using AuthRPolicy.MediatRExtensions.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.MediatRExtensions.Tests.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddAuthorizationBehaviorWithoutCustomCurrentUserService_ShouldRegisterServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddAuthorizationBehavior();

            // Assert
            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(ICurrentUserService) &&
                sd.ImplementationType == typeof(HttpContextBasedCurrentUserService) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IPipelineBehavior<,>) &&
                sd.ImplementationType == typeof(AuthorizationBehavior<,>) &&
                sd.Lifetime == ServiceLifetime.Scoped);
        }


        internal class CustomCurrentUserService : ICurrentUserService
        {
            public Task<User> GetCurrentUser(CancellationToken cancellationToken) => throw new NotImplementedException();
        }

        [Fact]
        public void AddAuthorizationBehaviorWithCustomCurrentUserService_ShouldRegisterServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddAuthorizationBehavior<CustomCurrentUserService>();

            // Assert
            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(ICurrentUserService) &&
                sd.ImplementationType == typeof(CustomCurrentUserService) &&
                sd.Lifetime == ServiceLifetime.Scoped);

            Assert.Contains(services, sd =>
                sd.ServiceType == typeof(IPipelineBehavior<,>) &&
                sd.ImplementationType == typeof(AuthorizationBehavior<,>) &&
                sd.Lifetime == ServiceLifetime.Scoped);
        }
    }
}
