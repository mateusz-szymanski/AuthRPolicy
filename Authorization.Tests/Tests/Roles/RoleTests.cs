using Authorization.Exceptions;
using Authorization.Permissions;
using Xunit;

namespace Authorization.Roles.Permissions.AccessPolicy
{
    public class RoleTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_ShouldThrowMissingRoleNameException_GivenEmptyName(string roleName)
        {
            // Arrange

            // Act
            // Assert
            Assert.Throws<MissingRoleNameException>(() => new Role(roleName));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("my-role")]
        [InlineData("my role")]
        [InlineData("my.role")]
        public void Constructor_ShouldCreateValidObject_GivenNonEmptyName(string roleName)
        {
            // Arrange

            // Act
            var role = new Role(roleName);

            // Assert
            Assert.Equal(roleName, role.Name);
        }
    }
}