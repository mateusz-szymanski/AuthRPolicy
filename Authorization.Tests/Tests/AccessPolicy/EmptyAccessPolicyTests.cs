using Authorization.AccessPolicy;
using Xunit;

namespace Authorization.Tests.Tests.AccessPolicy
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
