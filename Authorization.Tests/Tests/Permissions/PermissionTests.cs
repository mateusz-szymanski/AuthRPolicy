using Authorization.Exceptions;
using Authorization.Permissions;
using Xunit;

namespace Authorization.Roles.Permissions.AccessPolicy
{
    public class PermissionTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_ShouldThrowMissingPermissionNameException_GivenEmptyName(string permissionName)
        {
            // Arrange

            // Act
            // Assert
            Assert.Throws<MissingPermissionNameException>(() => new Permission(permissionName));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("my-permission")]
        [InlineData("my permission")]
        [InlineData("my.permission")]
        public void Constructor_ShouldCreateValidObject_GivenNonEmptyName(string permissionName)
        {
            // Arrange

            // Act
            var permission = new Permission(permissionName);

            // Assert
            Assert.Equal(permissionName, permission.Name);
        }
    }
}