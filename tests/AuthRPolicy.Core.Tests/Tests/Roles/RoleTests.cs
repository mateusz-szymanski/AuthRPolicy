using AuthRPolicy.Core.Roles;
using AuthRPolicy.Core.Roles.Exceptions;
using Xunit;

namespace AuthRPolicy.Core.Tests.Tests.Roles
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