using Authorization.AccessPolicy;
using Authorization.Exceptions;
using Authorization.Permissions;
using Authorization.Roles.Permissions;
using Authorization.Tests.Sample;
using Authorization.Tests.Sample.DocumentOwner;
using Moq;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Authorization.Roles.Permissions.AccessPolicy
{
    public class PermissionAccessPolicyTests
    {
        [Fact]
        public void Constructor_ShouldThrowMissingAccessPoliciesForPermissionAccessPolicyException_GivenPermissionAndNoAccessPolicies()
        {
            // Arrange
            var permission = new Permission("my-permission");
            var accessPolices = new IAccessPolicy[0];

            // Act
            // Assert
            Assert.Throws<MissingAccessPoliciesForPermissionAccessPolicyException>(
                () => new PermissionAccessPolicy(permission, accessPolices)
            );
        }

        [Fact]
        public void Constructor_ShouldCreateValidObject_GivenPermissionAndOneAccessPolicy()
        {
            // Arrange
            var permission = new Permission("my-permission");
            var accessPolicy1Mock = new Mock<IAccessPolicy>();
            accessPolicy1Mock.Setup(ap => ap.Name).Returns("access-policy-1");

            var accessPolices = new IAccessPolicy[]
            {
                accessPolicy1Mock.Object
            };

            // Act
            var permissionAccessPolicy = new PermissionAccessPolicy(permission, accessPolices);

            // Assert
            Assert.Equal(permission, permissionAccessPolicy.Permission);
            Assert.Equal(accessPolices, permissionAccessPolicy.AccessPolicies);
        }

        [Fact]
        public void Constructor_ShouldCreateValidObject_GivenPermissionAndTwoAccessPolicies()
        {
            // Arrange
            var permission = new Permission("my-permission");

            var accessPolicy1Mock = new Mock<IAccessPolicy>();
            accessPolicy1Mock.Setup(ap => ap.Name).Returns("access-policy-1");

            var accessPolicy2Mock = new Mock<IAccessPolicy>();
            accessPolicy2Mock.Setup(ap => ap.Name).Returns("access-policy-2");

            var accessPolices = new IAccessPolicy[]
            {
                accessPolicy1Mock.Object,
                accessPolicy2Mock.Object
            };

            // Act
            var permissionAccessPolicy = new PermissionAccessPolicy(permission, accessPolices);

            // Assert
            Assert.Equal(permission, permissionAccessPolicy.Permission);
            Assert.Equal(accessPolices, permissionAccessPolicy.AccessPolicies);
        }

    }
}