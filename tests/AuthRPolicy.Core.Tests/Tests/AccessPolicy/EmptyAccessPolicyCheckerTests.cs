using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Roles;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AuthRPolicy.Core.Tests.Tests.AccessPolicy
{
    public class EmptyAccessPolicyCheckerTests
    {
        [Theory]
        [InlineData("", new string[0])]
        [InlineData("my-username", new[] { "role-1" })]
        [InlineData("my-username", new[] { "role-1", "role-2" })]
        public void HasAccess_ShouldReturnTrue_GivenAnyUser(string userName, IEnumerable<string> roles)
        {
            // Arrange
            var userMock = new Mock<IUser>();
            userMock.Setup(u => u.Roles).Returns(roles.Select(r => new Role(r)));
            userMock.Setup(u => u.UserName).Returns(userName);

            var user = userMock.Object;

            var emptyAccessPolicy = new EmptyAccessPolicy();
            var emptyAccessPolicyChecker = new EmptyAccessPolicyChecker();

            // Act
            var hasAccess = emptyAccessPolicyChecker.HasAccess(user, emptyAccessPolicy);

            // Assert
            Assert.True(hasAccess);
        }
    }
}
