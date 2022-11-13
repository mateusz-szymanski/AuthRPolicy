using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Roles;
using AuthRPolicy.Core.Services;
using AuthRPolicy.MediatRExtensions.Exceptions;
using AuthRPolicy.MediatRExtensions.Services;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.MediatRExtensions.Tests.Tests
{
    public class AuthorizationBehaviorTests
    {
        public record AnonymousRequest : IRequest { }
        public record AuthorizedRequest : IRequest, IAuthorizedRequest
        {
            public PermissionAccessPolicy PermissionAccessPolicy { get; }

            public AuthorizedRequest(PermissionAccessPolicy permissionAccessPolicy) => PermissionAccessPolicy = permissionAccessPolicy;
        }

        [Fact]
        public async Task Handle_ShouldRunHandler_GivenAnonymousRequest()
        {
            // Arrange
            var authorizationServiceMock = new Mock<IAuthorizationService>();
            var currentUserServiceMock = new Mock<ICurrentUserService>();

            var logger = new NullLogger<AuthorizationBehavior<AnonymousRequest, Unit>>();
            var authorizationBehavior = new AuthorizationBehavior<AnonymousRequest, Unit>(
                logger,
                authorizationServiceMock.Object,
                currentUserServiceMock.Object);

            var request = new AnonymousRequest();
            var handled = false;

            RequestHandlerDelegate<Unit> requestHandlerDelegate = () =>
            {
                handled = true;
                return Unit.Task;
            };

            // Act
            await authorizationBehavior.Handle(request, requestHandlerDelegate, default);

            // Assert
            Assert.True(handled);
            currentUserServiceMock.Verify(s => s.GetCurrentUser(It.IsAny<CancellationToken>()), Times.Never);
            authorizationServiceMock.Verify(s => s.IsUserAuthorized(It.IsAny<User>(), It.IsAny<PermissionAccessPolicy>(), It.IsAny<CancellationToken>()), Times.Never);
            authorizationServiceMock.Verify(s => s.GetUserPermissions(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldRunHandler_GivenAuthorizedRequestAndIsAuthorizedUser()
        {
            // Arrange
            var user = new User("my-userName", new Role[0]);

            var authorizationServiceMock = new Mock<IAuthorizationService>();
            authorizationServiceMock
                .Setup(asm => asm.IsUserAuthorized(It.IsAny<User>(), It.IsAny<PermissionAccessPolicy>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(cusm => cusm.GetCurrentUser(It.IsAny<CancellationToken>())).ReturnsAsync(user);

            var logger = new NullLogger<AuthorizationBehavior<AuthorizedRequest, Unit>>();
            var authorizationBehavior = new AuthorizationBehavior<AuthorizedRequest, Unit>(
                logger,
                authorizationServiceMock.Object,
                currentUserServiceMock.Object);

            var permissionAccessPolicy = new PermissionAccessPolicy("permission-main-name", new EmptyAccessPolicy());

            var request = new AuthorizedRequest(permissionAccessPolicy);
            var handled = false;

            RequestHandlerDelegate<Unit> requestHandlerDelegate = () =>
            {
                handled = true;
                return Unit.Task;
            };

            // Act
            await authorizationBehavior.Handle(request, requestHandlerDelegate, default);

            // Assert
            Assert.True(handled);
            currentUserServiceMock.Verify(s => s.GetCurrentUser(It.IsAny<CancellationToken>()), Times.Once);
            authorizationServiceMock.Verify(s => s.IsUserAuthorized(user, permissionAccessPolicy, It.IsAny<CancellationToken>()), Times.Once);
            authorizationServiceMock.Verify(s => s.GetUserPermissions(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowUserUnauthorizedException_GivenAuthorizedRequestAndIsUnauthorizedUser()
        {
            // Arrange
            var user = new User("my-userName", new Role[0]);

            var authorizationServiceMock = new Mock<IAuthorizationService>();
            authorizationServiceMock
                .Setup(asm => asm.IsUserAuthorized(It.IsAny<User>(), It.IsAny<PermissionAccessPolicy>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(cusm => cusm.GetCurrentUser(It.IsAny<CancellationToken>())).ReturnsAsync(user);

            var logger = new NullLogger<AuthorizationBehavior<AuthorizedRequest, Unit>>();
            var authorizationBehavior = new AuthorizationBehavior<AuthorizedRequest, Unit>(
                logger,
                authorizationServiceMock.Object,
                currentUserServiceMock.Object);

            var permissionAccessPolicy = new PermissionAccessPolicy("permission-main-name", new EmptyAccessPolicy());

            var request = new AuthorizedRequest(permissionAccessPolicy);
            var handled = false;

            RequestHandlerDelegate<Unit> requestHandlerDelegate = () =>
            {
                handled = true;
                return Unit.Task;
            };

            // Act
            await Assert.ThrowsAsync<UserUnauthorizedException>(() => authorizationBehavior.Handle(request, requestHandlerDelegate, default));

            // Assert
            Assert.False(handled);
            currentUserServiceMock.Verify(s => s.GetCurrentUser(It.IsAny<CancellationToken>()), Times.Once);
            authorizationServiceMock.Verify(s => s.IsUserAuthorized(user, permissionAccessPolicy, It.IsAny<CancellationToken>()), Times.Once);
            authorizationServiceMock.Verify(s => s.GetUserPermissions(It.IsAny<User>()), Times.Never);
        }
    }
}
