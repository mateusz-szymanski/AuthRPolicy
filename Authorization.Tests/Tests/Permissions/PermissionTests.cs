using Authorization.AccessPolicy;
using Authorization.Exceptions;
using Authorization.Permissions;
using Xunit;

namespace Authorization.Tests.Tests.Permissions
{
    public class PermissionTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("   ", "")]
        [InlineData("", "sub-name")]
        [InlineData("   ", "sub-name")]
        public void Constructor_ShouldThrowMissingPermissionNameException_GivenEmptyName(string mainName, string subName)
        {
            // Arrange

            // Act
            // Assert
            Assert.Throws<MissingPermissionNameException>(() => new Permission<EmptyAccessPolicy>(mainName, subName));
        }

        [Theory]
        [InlineData("a", "", "a")]
        [InlineData("my-permission", "", "my-permission")]
        [InlineData("a", "sub-name", "a.sub-name")]
        [InlineData("my-permission", "sub-name", "my-permission.sub-name")]
        public void Constructor_ShouldCreateValidObject_GivenEmptyAccessPolicyType(string mainName, string subName, string expectedFullName)
        {
            // Arrange

            // Act
            var permission = new Permission<EmptyAccessPolicy>(mainName, subName);

            // Assert
            Assert.Equal(typeof(EmptyAccessPolicy), permission.AccessPolicyType);
            Assert.Equal(mainName, permission.MainName);
            Assert.Equal(subName, permission.SubName);
            Assert.Equal(expectedFullName, permission.FullName);
        }


        record CustomAccessPolicy(string Name) : IAccessPolicy;

        [Fact]
        public void Constructor_ShouldCreateValidObject_GivenCustomAccessPolicyType()
        {
            // Arrange
            var mainName = "main-name";
            var subName = "sub-name";

            // Act
            var permission = new Permission<CustomAccessPolicy>(mainName, subName);

            // Assert
            Assert.Equal(typeof(CustomAccessPolicy), permission.AccessPolicyType);
        }
    }
}