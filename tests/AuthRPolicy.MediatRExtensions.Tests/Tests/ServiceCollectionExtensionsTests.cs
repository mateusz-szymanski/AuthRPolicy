using AuthRPolicy.MediatRExtensions.IoC;
using AuthRPolicy.MediatRExtensions.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AuthRPolicy.MediatRExtensions.Tests.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddAuthorizationBehavior_ShouldRegisterServices()
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
    }
}
