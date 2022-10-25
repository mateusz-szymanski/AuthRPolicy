using AuthRPolicy.Core.Roles;
using AuthRPolicy.MediatRExtensions.Exceptions;
using AuthRPolicy.MediatRExtensions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.MediatRExtensions.Tests.Tests
{
    public class HttpContextBasedCurrentUserServiceTests
    {
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

            var logger = new NullLogger<HttpContextBasedCurrentUserService>();

            var httpContextBasedCurrentUserService = new HttpContextBasedCurrentUserService(httpContextAccessorMock.Object, logger);

            // Act
            var currentUser = await httpContextBasedCurrentUserService.GetCurrentUser(CancellationToken.None);

            // Assert
            Assert.Equal(userName, currentUser.UserName);
            Assert.Empty(currentUser.Roles);
        }

        [Fact]
        public async Task GetCurrentUser_ShouldReturnUser_GivenIdentityWithUserNameAndRolesClaims()
        {
            // Arrange
            var userName = "user-name-01";
            var role1 = new Role("role-1");
            var role2 = new Role("role-2");

            var claims = new Claim[]
            {
                new Claim("preferred_username", userName),
                new Claim("role", role1.Name),
                new Claim("role", role2.Name)
            };

            var identity = new ClaimsIdentity(claims);
            var contextUser = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext()
            {
                User = contextUser
            };
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(hcam => hcam.HttpContext).Returns(httpContext);

            var logger = new NullLogger<HttpContextBasedCurrentUserService>();

            var httpContextBasedCurrentUserService = new HttpContextBasedCurrentUserService(httpContextAccessorMock.Object, logger);

            // Act
            var currentUser = await httpContextBasedCurrentUserService.GetCurrentUser(CancellationToken.None);

            // Assert
            Assert.Equal(userName, currentUser.UserName);

            var expectedRoles = new[] { role1, role2 };
            Assert.Equal(expectedRoles, currentUser.Roles);
        }

        [Fact]
        public async Task GetCurrentUser_ShouldThrowMissingUserNameClaimException_GivenIdentityWithoutUserName()
        {
            // Arrange
            var role1 = new Role("role-1");
            var role2 = new Role("role-2");

            var claims = new Claim[]
            {
                new Claim("role", role1.Name),
                new Claim("role", role2.Name)
            };

            var identity = new ClaimsIdentity(claims);
            var contextUser = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext()
            {
                User = contextUser
            };
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(hcam => hcam.HttpContext).Returns(httpContext);

            var logger = new NullLogger<HttpContextBasedCurrentUserService>();

            var httpContextBasedCurrentUserService = new HttpContextBasedCurrentUserService(httpContextAccessorMock.Object, logger);

            // Act
            // Assert
            await Assert.ThrowsAsync<MissingUserNameClaimException>(
                async () => await httpContextBasedCurrentUserService.GetCurrentUser(CancellationToken.None)
            );
        }
    }
}
