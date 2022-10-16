using Authorization.AccessPolicy;
using Authorization.Permissions;
using Authorization.Roles;
using Authorization.Services;
using Authorization.Tests.Assertions;
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
        #region IsUserAuthorized

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public async Task IsUserAuthorized_ShouldReturnFalse_GivenUserWithoutAnyPermission(bool accessPolicy1HasAccess, bool accessPolicy2HasAccess)
        {
            // Arrange
            var actionPermission = new Permission<AccessPolicy1Stub>("my-permission-1");

            var roleProviderStub = new RoleProviderStub();

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new Role[0]);
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

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public async Task IsUserAuthorized_ShouldReturnFalse_GivenUserWithoutProperPermission(bool accessPolicy1HasAccess, bool accessPolicy2HasAccess)
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var actionPermission = new Permission<AccessPolicy1Stub>("my-permission-1");
            var userPermission = new Permission<AccessPolicy1Stub>("my-permission-2");

            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, userPermission);

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
            var permission = new Permission<AccessPolicy1Stub>("my-permission-1");
            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, permission);

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
        public async Task IsUserAuthorized_ShouldReturnFalse_GivenUserWithProperSubPermissionButWithAccessPolicyReturnFalse()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permissionMainName = "my-permission";

            var permission1 = new Permission<AccessPolicy1Stub>(permissionMainName, "1");
            var permission2 = new Permission<AccessPolicy2Stub>(permissionMainName, "2");
            var permission3 = new Permission<AccessPolicy3Stub>(permissionMainName, "3");

            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, permission1);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1 });
            var user = userMock.Object;

            var accessPolicy1Stub = new AccessPolicy1Stub();
            var accessPolicy1CheckerStub = new AccessPolicy1CheckerStub()
                .ShouldReturn((_, _) => false);

            var accessPolicy2Stub = new AccessPolicy2Stub();
            var accessPolicy2CheckerStub = new AccessPolicy2CheckerStub()
                .ShouldReturn((_, _) => true);

            var accessPolicy3Stub = new AccessPolicy3Stub();
            var accessPolicy3CheckerStub = new AccessPolicy3CheckerStub()
                .ShouldReturn((_, _) => true);

            var permissionAccessPolicy = new PermissionAccessPolicy(permissionMainName, accessPolicy1Stub, accessPolicy2Stub, accessPolicy3Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
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
            Assert.False(isUserAuthorized);
        }

        [Fact]
        public async Task IsUserAuthorized_ShouldReturnFalse_GivenUserWithProperMulitpleSubPermissionsButWithAllAccessPoliciesReturnFalse()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permissionMainName = "my-permission";

            var permission1 = new Permission<AccessPolicy1Stub>(permissionMainName, "1");
            var permission2 = new Permission<AccessPolicy2Stub>(permissionMainName, "2");
            var permission3 = new Permission<AccessPolicy3Stub>(permissionMainName, "3");

            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, permission1, permission2, permission3);

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

            var accessPolicy3Stub = new AccessPolicy3Stub();
            var accessPolicy3CheckerStub = new AccessPolicy3CheckerStub()
                .ShouldReturn((_, _) => false);

            var permissionAccessPolicy = new PermissionAccessPolicy(permissionMainName, accessPolicy1Stub, accessPolicy2Stub, accessPolicy3Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
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
            Assert.False(isUserAuthorized);
        }

        [Fact]
        public async Task IsUserAuthorized_ShouldReturnTrue_GivenUserWithProperPermissionAndAccessPolicyReturnTrue()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<AccessPolicy1Stub>("my-permission-1");

            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, permission1);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1 });
            var user = userMock.Object;

            var accessPolicy1Stub = new AccessPolicy1Stub();
            var accessPolicy1CheckerStub = new AccessPolicy1CheckerStub()
                .ShouldReturn((_, _) => true);

            var permissionAccessPolicy = new PermissionAccessPolicy(permission1.MainName, accessPolicy1Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IRoleProvider>(roleProviderStub)
                .AddSingleton<IAccessPolicyChecker<AccessPolicy1Stub>>(accessPolicy1CheckerStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var isUserAuthorized = authorizationService.IsUserAuthorized(user, permissionAccessPolicy);

            // Assert
            Assert.True(isUserAuthorized);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, true, false)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        public async Task IsUserAuthorized_ShouldReturnTrue_GivenUserWithProperSubPermissionWithAtLeastOneAccessPolicyReturnTrue(
            bool accessPolicy1HasAccess, bool accessPolicy2HasAccess, bool accessPolicy3HasAccess)
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permissionMainName = "my-permission";

            var permission1 = new Permission<AccessPolicy1Stub>(permissionMainName, "1");
            var permission2 = new Permission<AccessPolicy2Stub>(permissionMainName, "2");
            var permission3 = new Permission<AccessPolicy3Stub>(permissionMainName, "3");

            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, permission1, permission2, permission3);

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

            var permissionAccessPolicy = new PermissionAccessPolicy(permissionMainName, accessPolicy1Stub, accessPolicy2Stub, accessPolicy3Stub);

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
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
            Assert.True(isUserAuthorized);
        }

        #endregion

        #region GetUserPermissions

        [Fact]
        public async Task GetUserPermissions_ShouldReturnEmptyList_GivenUserWithoutAnyPermission()
        {
            // Arrange
            var userRole = new Role("my-role-1");

            var roleProviderStub = new RoleProviderStub()
                .AddRole(userRole);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { userRole });
            var user = userMock.Object;

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IRoleProvider>(roleProviderStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var userPermissions = authorizationService.GetUserPermissions(user);

            // Assert
            Assert.Empty(userPermissions);
        }

        [Fact]
        public async Task GetUserPermissions_ShouldReturnEmptyList_GivenUserWithNonExistingRole()
        {
            // Arrange
            var userRole = new Role("my-role-1");
            var otherRole = new Role("my-role-2");

            var roleProviderStub = new RoleProviderStub()
                .AddRole(otherRole);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { userRole });
            var user = userMock.Object;

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IRoleProvider>(roleProviderStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var userPermissions = authorizationService.GetUserPermissions(user);

            // Assert
            Assert.Empty(userPermissions);
        }

        [Fact]
        public async Task GetUserPermissions_ShouldReturnPermissions_GivenUserWithExistingRole()
        {
            // Arrange
            var userRole = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<EmptyAccessPolicy>("my-permission-2");
            var permission3 = new Permission<EmptyAccessPolicy>("my-permission-3");
            var permission4 = new Permission<EmptyAccessPolicy>("my-permission-1");

            var roleProviderStub = new RoleProviderStub()
                .AddRole(userRole, permission1, permission2, permission3, permission4);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { userRole });
            var user = userMock.Object;

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IRoleProvider>(roleProviderStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var actualPermissions = authorizationService.GetUserPermissions(user);

            // Assert
            var expectedPermissions = new[]
            {
                permission1, permission2, permission3
            };

            AssertPermission.Equal(expectedPermissions, actualPermissions);
        }

        [Fact]
        public async Task GetUserPermissions_ShouldReturnPermissions_GivenUserWithTwoRolesWithOverlappingPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<EmptyAccessPolicy>("my-permission-2");
            var permission3 = new Permission<AccessPolicy1Stub>("my-permission-3");
            var permission4 = new Permission<AccessPolicy2Stub>("my-permission-4");
            var permission5 = new Permission<EmptyAccessPolicy>("my-permission-5");

            var role1Permissions = new IPermission[] {
                permission1,
                permission2,
                permission3
            };
            var role2 = new Role("my-role-2");
            var role2Permissions = new IPermission[] {
                permission5,
                permission4,
                permission3,
                permission1
            };

            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, role1Permissions)
                .AddRole(role2, role2Permissions);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1, role2 });
            var user = userMock.Object;

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IRoleProvider>(roleProviderStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var actualPermissions = authorizationService.GetUserPermissions(user);

            // Assert
            var expectedPermissions = new IPermission[]
            {
                permission1,
                permission2,
                permission3,
                permission4,
                permission5
            };

            AssertPermission.Equal(expectedPermissions, actualPermissions);
        }


        // TODO: Some of the tests are for AbstractRoleProvider, not AuthorizationService.
        // TODO: Add other roles to the system to make sure that the serivce is not taking all that exists
        [Fact]
        public async Task GetUserPermissions_ShouldReturnPermissions_GivenUserWithTwoRolesAndConnectedPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<EmptyAccessPolicy>("my-permission-2");
            var permission3 = new Permission<AccessPolicy1Stub>("my-permission-3");
            var permission4 = new Permission<AccessPolicy2Stub>("my-permission-4");
            var permission5 = new Permission<EmptyAccessPolicy>("my-permission-5");
            var permission6 = new Permission<EmptyAccessPolicy>("my-permission-6");
            var permission7 = new Permission<AccessPolicy2Stub>("my-permission-7");
            var permission8 = new Permission<AccessPolicy1Stub>("my-permission-8");
            var permission9 = new Permission<EmptyAccessPolicy>("my-permission-9");
            var permission10 = new Permission<EmptyAccessPolicy>("my-permission-10");

            var role1Permissions = new IPermission[] {
                permission1,
                permission2,
                permission3
            };
            var role2 = new Role("my-role-2");
            var role2Permissions = new IPermission[] {
                permission5,
                permission4,
                permission3,
                permission1
            };

            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, role1Permissions)
                .AddRole(role2, role2Permissions)
                .ConnectPermissions(permission1, permission6, permission7)
                .ConnectPermissions(permission7, permission8)
                .ConnectPermissions(permission8, permission9)
                .ConnectPermissions(permission9, permission10);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1, role2 });
            var user = userMock.Object;

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IRoleProvider>(roleProviderStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var actualPermissions = authorizationService.GetUserPermissions(user);

            // Assert
            var expectedPermissions = new IPermission[]
            {
                permission1,
                permission2,
                permission3,
                permission4,
                permission5,
                permission6,
                permission7,
                permission8,
                permission9,
                permission10
            };

            AssertPermission.Equal(expectedPermissions, actualPermissions);
        }

        [Fact]
        public async Task GetUserPermissions_ShouldReturnPermissions_GivenUserWithTwoRolesAndCircularConnectedPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<EmptyAccessPolicy>("my-permission-2");
            var permission3 = new Permission<AccessPolicy1Stub>("my-permission-3");

            var role1Permissions = new IPermission[] {
                permission1,
                permission2,
            };
            var role2 = new Role("my-role-2");
            var role2Permissions = new IPermission[] {
                permission3
            };

            var roleProviderStub = new RoleProviderStub()
                .AddRole(role1, role1Permissions)
                .AddRole(role2, role2Permissions)
                .ConnectPermissions(permission1, permission2, permission3)
                .ConnectPermissions(permission2, permission1);

            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.UserName).Returns("my-username");
            userMock.Setup(u => u.Roles).Returns(new[] { role1, role2 });
            var user = userMock.Object;

            var serviceCollection = new ServiceCollection()
                .AddNullLogger()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddSingleton<IRoleProvider>(roleProviderStub);

            await using var serviceProvider = serviceCollection.BuildServiceProvider();
            await using var serviceScope = serviceProvider.CreateAsyncScope();

            var authorizationService = serviceScope.ServiceProvider.GetRequiredService<IAuthorizationService>();

            // Act
            var actualPermissions = authorizationService.GetUserPermissions(user);

            // Assert
            var expectedPermissions = new IPermission[]
            {
                permission1,
                permission2,
                permission3
            };

            AssertPermission.Equal(expectedPermissions, actualPermissions);
        }

        #endregion

    }
}