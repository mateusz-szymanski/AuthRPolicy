using AuthRPolicy.Core.Services;
using AuthRPolicy.MediatRExtensions.Exceptions;
using AuthRPolicy.MediatRExtensions.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthRPolicy.MediatRExtensions
{
    internal class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<AuthorizationBehavior<TRequest, TResponse>> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehavior(
            ILogger<AuthorizationBehavior<TRequest, TResponse>> logger,
            IAuthorizationService authorizationService,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IAuthorizedRequest authorizedRequest)
            {
                _logger.LogDebug("Getting current user to authorize {request}", request.GetType().Name);

                var currentUser = await _currentUserService.GetCurrentUser(cancellationToken);

                _logger.LogDebug("Authorizing user {userName} to execute {request}", currentUser.UserName, request.GetType().Name);

                var isUserAuthorized = await _authorizationService.IsUserAuthorized(
                    currentUser,
                    authorizedRequest.PermissionAccessPolicy,
                    CancellationToken.None);

                if (!isUserAuthorized)
                    throw UserUnauthorizedException.New(currentUser, authorizedRequest);

                _logger.LogInformation("User {userName} is authorized to execute {request}", currentUser.UserName, request.GetType().Name);
            }

            var response = await next();

            return response;
        }
    }
}
