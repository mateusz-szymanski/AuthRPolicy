using AuthRAccessPolicy.Core.AccessPolicy;
using Xunit;

namespace AuthRAccessPolicy.Core.Tests.Tests.AccessPolicy
{
    public class EmptyAccessPolicyTests
    {
        [Fact]
        public void Name_ShouldBeSetToEmpty()
        {
            // Arrange
            // Act
            var policy = new EmptyAccessPolicy();

            // Assert
            Assert.Equal("Empty", policy.Name);
        }
    }
}
