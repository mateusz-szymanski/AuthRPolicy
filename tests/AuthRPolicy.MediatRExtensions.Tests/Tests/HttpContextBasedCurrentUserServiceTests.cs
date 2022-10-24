using AuthRPolicy.MediatRExtensions.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.MediatRExtensions.Tests.Tests
{
    public class HttpContextBasedCurrentUserServiceTests
    {
        // TODO:
        [Fact]
        public async Task GetCurrentUser_ShouldReturnUser_GivenIdentityWithUserNameClaimOnly()
        {
            // Arrange
            var userName = "user-name-01";
            var claims = new Claim[]
            {
                new Claim("preferred_username", userName)
            };

            var identity = new ClaimsIdentity(claims);
            var contextUser = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext()
            {
                User = contextUser
            };
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(hcam => hcam.HttpContext).Returns(httpContext);

            var httpContextBasedCurrentUserService = new HttpContextBasedCurrentUserService(httpContextAccessorMock.Object);

            // Act
            var currentUser = await httpContextBasedCurrentUserService.GetCurrentUser(CancellationToken.None);

            // Assert
            Assert.Equal(userName, currentUser.UserName);
            Assert.Empty(currentUser.Roles);
        }
    }
}
