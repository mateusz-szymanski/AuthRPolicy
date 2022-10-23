using AuthRPolicy.Core;
using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Services;
using AuthRPolicy.MediatRExtensions.Services;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.MediatRExtensions.Tests.Tests
{
    // TODO: 
    public class AuthorizationBehaviorTests
    {
        public record AnonymousRequest : IRequest { }
        //public record AuthorizedRequest : IAuthorizedRequest
        //{
        //    public PermissionAccessPolicy PermissionAccessPolicy => new PermissionAccessPolicy("my-permission", ...);
        //}

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
            authorizationServiceMock.Verify(s => s.GetUserPermissions(It.IsAny<User>()), Times.Never);
            authorizationServiceMock.Verify(s => s.IsUserAuthorized(It.IsAny<User>(), It.IsAny<PermissionAccessPolicy>()), Times.Never);
        }
    }
}
