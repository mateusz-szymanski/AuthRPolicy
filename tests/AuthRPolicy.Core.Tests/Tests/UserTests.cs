using AuthRPolicy.Core.Exceptions;
using AuthRPolicy.Core.Roles;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AuthRPolicy.Core.Tests.Tests
{
    public class UserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_ShouldThrowMissingUserNameException_GivenEmptyUserName(string userName)
        {
            // Arrange

            // Act
            // Assert
            Assert.Throws<MissingUserNameException>(() => new User(userName, new Role[0]));
        }

        [Theory]
        [InlineData("a", new string[0])]
        [InlineData("a", new[] { "role-1" })]
        [InlineData("a", new[] { "role-1", "role-2" })]
        [InlineData("my-username", new[] { "role-1" })]
        [InlineData("my.username", new[] { "role-1", "role-2", "role-3" })]
        [InlineData("my username", new[] { "role-1", "role-3", "role-2", "role-1" })]
        public void Constructor_ShouldCreateValidObject(string userName, IEnumerable<string> rolesNames)
        {
            // Arrange
            var roles = rolesNames.Select(rn => new Role(rn));

            // Act
            var user = new User(userName, roles);

            // Assert
            Assert.Equal(userName, user.UserName);

            var expectedRolesNames = rolesNames.Distinct().OrderBy(rn => rn);
            var actualRoleNames = user.Roles.Select(r => r.Name).OrderBy(rn => rn);

            Assert.Equal(expectedRolesNames, actualRoleNames);
        }
    }
}