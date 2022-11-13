using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Roles;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.Core.Tests.Tests.AccessPolicy
{
    public class EmptyAccessPolicyCheckerTests
    {
        [Theory]
        [InlineData("my-username", new string[0])]
        [InlineData("my-username", new[] { "role-1" })]
        [InlineData("my-username", new[] { "role-1", "role-2" })]
        public async Task HasAccess_ShouldReturnTrue_GivenAnyUser(string userName, IEnumerable<string> roles)
        {
            // Arrange
            var user = new User(userName, roles.Select(r => new Role(r)));

            var emptyAccessPolicy = new EmptyAccessPolicy();
            var emptyAccessPolicyChecker = new EmptyAccessPolicyChecker();

            // Act
            var hasAccess = await emptyAccessPolicyChecker.HasAccess(user, emptyAccessPolicy, CancellationToken.None);

            // Assert
            Assert.True(hasAccess);
        }
    }
}
