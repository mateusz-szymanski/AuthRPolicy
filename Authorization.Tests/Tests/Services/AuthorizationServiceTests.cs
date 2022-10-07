using Authorization.AccessPolicy;
using Authorization.Permissions;
using Authorization.Roles;
using Authorization.Services;
using Authorization.Tests.IoC;
using Authorization.Tests.Stubs;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Authorization.Tests.Tests.Services
{
    public class AuthorizationServiceTests
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public async Task IsUserAuthorized_ShouldReturnFalse_GivenUserWithoutProperPermission(bool accessPolicy1HasAccess, bool accessPolicy2HasAccess)
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var roleProviderStub = new RoleProviderStub()
                .WithRoles(role1);

            var actionPermission = new Permission<AccessPolicy1Stub>("my-permission-1");
            var userPermission = new Permission<AccessPolicy1Stub>("my-permission-2");

            var permissionProviderStub = new PermissionProviderStub()
                .WithRole(role1, userPermission);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1 });
            var user = userMock.Object;

            var accessPolicy1Stub = new AccessPolicy1Stub();
            var accessPolicy1CheckerStub = new AccessPolicy1CheckerStub()
                .ShouldReturn((_, _) => accessPolicy1HasAccess);

            var accessPolicy2Stub = new AccessPolicy2Stub();
            var accessPolicy2CheckerStub = new AccessPolicy2CheckerStub()
                .ShouldReturn((_, _) => accessPolicy2HasAccess);

            var permissionAccessPolicy = new PermissionAccessPolicy(actionPermission.MainName, accessPolicy1Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IPermissionProvider>(permissionProviderStub)
                .AddSingleton<IRoleProvider>(roleProviderStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy1Stub>>(accessPolicy1CheckerStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy2Stub>>(accessPolicy2CheckerStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var isUserAuthorized = authorizationService.IsUserAuthorized(user, permissionAccessPolicy);

            // Assert
            Assert.False(isUserAuthorized);
        }

        [Fact]
        public async Task IsUserAuthorized_ShouldReturnFalse_GivenUserWithProperPermissionButWithAccessPolicyReturnFalse()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var roleProviderStub = new RoleProviderStub()
                .WithRoles(role1);

            var permission = new Permission<AccessPolicy1Stub>("my-permission-1");

            var permissionProviderStub = new PermissionProviderStub()
                .WithRole(role1, permission);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1 });
            var user = userMock.Object;

            var accessPolicy1Stub = new AccessPolicy1Stub();
            var accessPolicy1CheckerStub = new AccessPolicy1CheckerStub()
                .ShouldReturn((_, _) => false);

            var accessPolicy2Stub = new AccessPolicy2Stub();
            var accessPolicy2CheckerStub = new AccessPolicy2CheckerStub()
                .ShouldReturn((_, _) => false);

            var permissionAccessPolicy = new PermissionAccessPolicy(permission.MainName, accessPolicy1Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IPermissionProvider>(permissionProviderStub)
                .AddSingleton<IRoleProvider>(roleProviderStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy1Stub>>(accessPolicy1CheckerStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy2Stub>>(accessPolicy2CheckerStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var isUserAuthorized = authorizationService.IsUserAuthorized(user, permissionAccessPolicy);

            // Assert
            Assert.False(isUserAuthorized);
        }
    }
}