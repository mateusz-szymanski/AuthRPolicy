using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Roles;
using AuthRPolicy.Core.Roles.Exceptions;
using AuthRPolicy.Core.Tests.Assertions;
using AuthRPolicy.Core.Tests.Stubs;
using System.Linq;
using Xunit;

namespace AuthRPolicy.Core.Tests.Tests.Roles
{
    public class DefaultRoleProviderTests
    {
        #region AddRole

        [Fact]
        public void AddRole_ShouldThrowRoleAlreadyAddedException_GivenTwoRoleWithTheSameName()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var role2 = new Role("my-role-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(role1);

            // Act
            // Assert
            Assert.Throws<RoleAlreadyAddedException>(() => defaultRoleProvider.AddRole(role2));
        }

        #endregion

        #region ConnectPermissions

        [Fact]
        public void ConnectPermissions_ShouldThrowPermissionAlreadyConnectedException_GivenTwoPermissionWithTheSameName()
        {
            // Arrange
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var connectedPermission = new Permission<EmptyAccessPolicy>("my-permission-10");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.ConnectPermissions(permission1, connectedPermission);

            // Act
            // Assert
            Assert.Throws<PermissionAlreadyConnectedException>(() => defaultRoleProvider.ConnectPermissions(permission2, connectedPermission));
        }

        #endregion

        #region GetAvailableRoles

        [Fact]
        public void GetAvailableRoles_ShouldReturnEmptyList_GivenNoRoles()
        {
            // Arrange
            var defaultRoleProvider = new DefaultRoleProvider();

            // Act
            var roles = defaultRoleProvider.GetAvailableRoles();

            // Assert
            Assert.Empty(roles);
        }

        [Fact]
        public void GetAvailableRoles_ShouldReturnAllAddedRoles_GivenThreeRolesWithNoPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var role2 = new Role("my-role-2");
            var role3 = new Role("my-role-3");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(role1);
            defaultRoleProvider.AddRole(role2);
            defaultRoleProvider.AddRole(role3);

            // Act
            var roles = defaultRoleProvider.GetAvailableRoles();

            // Assert
            Assert.Equal(3, roles.Count());
            Assert.Contains(role1, roles);
            Assert.Contains(role2, roles);
            Assert.Contains(role3, roles);
        }

        [Fact]
        public void GetAvailableRoles_ShouldReturnAllAddedRoles_GivenTwoRolesWithPermissionsAndOneWithout()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var role2 = new Role("my-role-2");
            var role3 = new Role("my-role-3");

            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<EmptyAccessPolicy>("my-permission-2");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(role1);
            defaultRoleProvider.AddRole(role2, permission1);
            defaultRoleProvider.AddRole(role3, permission2);

            // Act
            var roles = defaultRoleProvider.GetAvailableRoles();

            // Assert
            Assert.Equal(3, roles.Count());
            Assert.Contains(role1, roles);
            Assert.Contains(role2, roles);
            Assert.Contains(role3, roles);
        }

        #endregion

        #region GetPermissionsForRole

        [Fact]
        public void GetPermissionsForRole_ShouldReturnEmptyList_GivenUnknownRole()
        {
            // Arrange
            var role1 = new Role("my-role-1");

            var otherRole1 = new Role("other-role-1");
            var otherPermission1 = new Permission<EmptyAccessPolicy>("other-permission-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(otherRole1, otherPermission1);

            // Act
            var permissions = defaultRoleProvider.GetPermissionsForRole(role1);

            // Assert
            Assert.Empty(permissions);
        }

        [Fact]
        public void GetPermissionsForRole_ShouldReturnEmptyList_GivenRoleWithNoPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");

            var otherRole1 = new Role("other-role-1");
            var otherPermission1 = new Permission<EmptyAccessPolicy>("other-permission-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(otherRole1, otherPermission1);
            defaultRoleProvider.AddRole(role1);

            // Act
            var permissions = defaultRoleProvider.GetPermissionsForRole(role1);

            // Assert
            Assert.Empty(permissions);
        }

        [Fact]
        public void GetPermissionsForRole_ShouldReturnRolePermissions_GivenRoleWithOnePermission()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");

            var otherRole1 = new Role("other-role-1");
            var otherPermission1 = new Permission<EmptyAccessPolicy>("other-permission-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(otherRole1, otherPermission1);
            defaultRoleProvider.AddRole(role1, permission1);

            // Act
            var permissions = defaultRoleProvider.GetPermissionsForRole(role1);

            // Assert
            var permission = Assert.Single(permissions);
            Assert.Equal(permission1, permission);
        }

        [Fact]
        public void GetPermissionsForRole_ShouldReturnRolePermissions_GivenRoleWithTwoPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<AccessPolicy1Stub>("my-permission-2");

            var otherRole1 = new Role("other-role-1");
            var otherPermission1 = new Permission<EmptyAccessPolicy>("other-permission-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(otherRole1, otherPermission1);
            defaultRoleProvider.AddRole(role1, permission1, permission2);

            // Act
            var permissions = defaultRoleProvider.GetPermissionsForRole(role1);

            // Assert
            var expectedPermissions = new IPermission[]
            {
                permission1,
                permission2
            };

            AssertPermission.Equal(expectedPermissions, permissions);
        }

        [Fact]
        public void GetPermissionsForRole_ShouldReturnRolePermissions_GivenRoleWithTwoPermissionsAndOneLevelOfAdditionalPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<AccessPolicy1Stub>("my-permission-2");
            var permission3 = new Permission<AccessPolicy2Stub>("my-permission-3");
            var permission4 = new Permission<EmptyAccessPolicy>("my-permission-4");
            var permission5 = new Permission<EmptyAccessPolicy>("my-permission-5");
            var permission6 = new Permission<EmptyAccessPolicy>("my-permission-6");

            var otherRole1 = new Role("other-role-1");
            var otherPermission1 = new Permission<EmptyAccessPolicy>("other-permission-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(otherRole1, otherPermission1);

            defaultRoleProvider.AddRole(role1, permission1, permission2);
            defaultRoleProvider.ConnectPermissions(permission1, permission3, permission4);
            defaultRoleProvider.ConnectPermissions(permission2, permission5, permission6);

            // Act
            var permissions = defaultRoleProvider.GetPermissionsForRole(role1);

            // Assert
            var expectedPermissions = new IPermission[]
            {
                permission1,
                permission2,
                permission3,
                permission4,
                permission5,
                permission6
            };

            AssertPermission.Equal(expectedPermissions, permissions);
        }

        [Fact]
        public void GetPermissionsForRole_ShouldReturnRolePermissions_GivenRoleWithTwoPermissionsAndThreeLevelsOfAdditionalPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<AccessPolicy1Stub>("my-permission-2");
            var permission3 = new Permission<AccessPolicy2Stub>("my-permission-3");
            var permission4 = new Permission<EmptyAccessPolicy>("my-permission-4");
            var permission5 = new Permission<AccessPolicy1Stub>("my-permission-5");
            var permission6 = new Permission<AccessPolicy2Stub>("my-permission-6");
            var permission7 = new Permission<EmptyAccessPolicy>("my-permission-7");
            var permission8 = new Permission<EmptyAccessPolicy>("my-permission-8");
            var permission9 = new Permission<EmptyAccessPolicy>("my-permission-9");

            var otherRole1 = new Role("other-role-1");
            var otherPermission1 = new Permission<EmptyAccessPolicy>("other-permission-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(otherRole1, otherPermission1);

            defaultRoleProvider.AddRole(role1, permission1, permission2);
            defaultRoleProvider.ConnectPermissions(permission1, permission3, permission4);
            defaultRoleProvider.ConnectPermissions(permission2, permission5);
            defaultRoleProvider.ConnectPermissions(permission3, permission6, permission7);
            defaultRoleProvider.ConnectPermissions(permission5, permission8);
            defaultRoleProvider.ConnectPermissions(permission8, permission9);

            // Act
            var permissions = defaultRoleProvider.GetPermissionsForRole(role1);

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
            };

            AssertPermission.Equal(expectedPermissions, permissions);
        }

        [Fact]
        public void GetPermissionsForRole_ShouldReturnRolePermissions_GivenDuplicatesInPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<AccessPolicy1Stub>("my-permission-2");
            var permission3 = new Permission<AccessPolicy2Stub>("my-permission-3");
            var permission4 = new Permission<EmptyAccessPolicy>("my-permission-4");
            var permission5 = new Permission<EmptyAccessPolicy>("my-permission-5");
            var permission6 = new Permission<AccessPolicy1Stub>("my-permission-6");
            var permission7 = new Permission<AccessPolicy2Stub>("my-permission-7");
            var permission8 = new Permission<EmptyAccessPolicy>("my-permission-8");
            var permission9 = new Permission<EmptyAccessPolicy>("my-permission-9");

            var otherRole1 = new Role("other-role-1");
            var otherPermission1 = new Permission<EmptyAccessPolicy>("other-permission-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(otherRole1, otherPermission1);

            defaultRoleProvider.AddRole(role1, permission1, permission2);
            defaultRoleProvider.ConnectPermissions(permission1, permission2, permission3, permission4);
            defaultRoleProvider.ConnectPermissions(permission2, permission5, permission3, permission6);
            defaultRoleProvider.ConnectPermissions(permission3, permission7);
            defaultRoleProvider.ConnectPermissions(permission5, permission8, permission9);
            defaultRoleProvider.ConnectPermissions(permission8, permission9);

            // Act
            var permissions = defaultRoleProvider.GetPermissionsForRole(role1);

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
            };

            AssertPermission.Equal(expectedPermissions, permissions);
        }

        [Fact]
        public void GetPermissionsForRole_ShouldReturnRolePermissions_GivenCircularAdditionalPermissions()
        {
            // Arrange
            var role1 = new Role("my-role-1");
            var permission1 = new Permission<EmptyAccessPolicy>("my-permission-1");
            var permission2 = new Permission<AccessPolicy1Stub>("my-permission-2");
            var permission3 = new Permission<EmptyAccessPolicy>("my-permission-3");

            var otherRole1 = new Role("other-role-1");
            var otherPermission1 = new Permission<EmptyAccessPolicy>("other-permission-1");

            var defaultRoleProvider = new DefaultRoleProvider();
            defaultRoleProvider.AddRole(otherRole1, otherPermission1);

            defaultRoleProvider.AddRole(role1, permission1);
            defaultRoleProvider.ConnectPermissions(permission1, permission2);
            defaultRoleProvider.ConnectPermissions(permission2, permission3);
            defaultRoleProvider.ConnectPermissions(permission3, permission1);

            // Act
            var permissions = defaultRoleProvider.GetPermissionsForRole(role1);

            // Assert
            var expectedPermissions = new IPermission[]
            {
                permission1,
                permission2,
                permission3,
            };

            AssertPermission.Equal(expectedPermissions, permissions);
        }

        #endregion
    }
}