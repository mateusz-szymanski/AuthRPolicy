using Authorization.AccessPolicy;
using Authorization.IoC;
using Authorization.Permissions;
using Authorization.Services;
using Authorization.Tests.Stubs;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Authorization.Roles.Permissions.AccessPolicy
{
    public class AuthorizationServiceTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task IsUserAuthorized_ShouldReturnFalse_GivenUserWithoutProperPermission(bool accessPolicyHasAccess)
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var roleProviderStub = new RoleProviderStub()
                .WithRoles(role1);

            var permission1 = new Permission("my-permission-1");
            var permission2 = new Permission("my-permission-2");

            var permissionProviderStub = new PermissionProviderStub()
                .WithRole(role1, permission1);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1 });
            var user = userMock.Object;

            var accessPolicy1Stub = new AccessPolicy1Stub();
            var accessPolicy1CheckerStub = new AccessPolicy1CheckerStub()
                .ShouldReturn((_, _) => accessPolicyHasAccess);

            var permissionAccessPolicy = new PermissionAccessPolicy(permission2, accessPolicy1Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IPermissionProvider>(permissionProviderStub)
                .AddSingleton<IRoleProvider>(roleProviderStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy1Stub>>(accessPolicy1CheckerStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var isUserAuthorized = authorizationService.IsUserAuthorized(user, permissionAccessPolicy);

            // Assert
            Assert.False(isUserAuthorized);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async Task IsUserAuthorized_ShouldReturnTrue_GivenUserWithCorrectPermissionAndOnePermissionAccessPolicyCheckerThatReturnsTrue
            (bool accessPolicyHasAccess, bool expectedHasAccess)
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var roleProviderStub = new RoleProviderStub()
                .WithRoles(role1);

            var permission1 = new Permission("my-permission-1");
            var permissionProviderStub = new PermissionProviderStub()
                .WithRole(role1, permission1);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1 });
            var user = userMock.Object;

            var accessPolicy1Stub = new AccessPolicy1Stub();
            var accessPolicy1CheckerStub = new AccessPolicy1CheckerStub()
                .ShouldReturn((_, _) => accessPolicyHasAccess);

            var permissionAccessPolicy = new PermissionAccessPolicy(permission1, accessPolicy1Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IPermissionProvider>(permissionProviderStub)
                .AddSingleton<IRoleProvider>(roleProviderStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy1Stub>>(accessPolicy1CheckerStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var isUserAuthorized = authorizationService.IsUserAuthorized(user, permissionAccessPolicy);

            // Assert
            Assert.Equal(expectedHasAccess, isUserAuthorized);
        }

        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(true, true, false, true)]
        [InlineData(true, false, true, true)]
        [InlineData(false, true, true, true)]
        [InlineData(true, false, false, true)]
        [InlineData(false, false, true, true)]
        [InlineData(false, true, false, true)]
        [InlineData(false, false, false, false)]
        public async Task IsUserAuthorized_ShouldReturnTrue_GivenUserWithCorrectPermissionAndAtLeastOnePermissionAccessPolicyCheckerThatReturnsTrue
            (bool accessPolicy1HasAccess, bool accessPolicy2HasAccess, bool accessPolicy3HasAccess, bool expectedHasAccess)
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var roleProviderStub = new RoleProviderStub()
                .WithRoles(role1);

            var permission1 = new Permission("my-permission-1");
            var permissionProviderStub = new PermissionProviderStub()
                .WithRole(role1, permission1);

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

            var accessPolicy3Stub = new AccessPolicy3Stub();
            var accessPolicy3CheckerStub = new AccessPolicy3CheckerStub()
                .ShouldReturn((_, _) => accessPolicy3HasAccess);

            var permissionAccessPolicy = new PermissionAccessPolicy(permission1, accessPolicy1Stub, accessPolicy2Stub, accessPolicy3Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IPermissionProvider>(permissionProviderStub)
                .AddSingleton<IRoleProvider>(roleProviderStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy1Stub>>(accessPolicy1CheckerStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy2Stub>>(accessPolicy2CheckerStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy3Stub>>(accessPolicy3CheckerStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var isUserAuthorized = authorizationService.IsUserAuthorized(user, permissionAccessPolicy);

            // Assert
            Assert.Equal(expectedHasAccess, isUserAuthorized);
        }
    }
}