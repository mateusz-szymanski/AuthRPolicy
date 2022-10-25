using AuthRPolicy.Core;
using AuthRPolicy.Core.Roles;
using AuthRPolicy.MediatRExtensions.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AuthRPolicy.MediatRExtensions.Services
{
    internal class HttpContextBasedCurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HttpContextBasedCurrentUserService> _logger;

        public HttpContextBasedCurrentUserService(
            IHttpContextAccessor httpContextAccessor,
            ILogger<HttpContextBasedCurrentUserService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public Task<User> GetCurrentUser(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;

            var userNameClaimType = "preferred_username";
            var roleClaimType = "role";

            // TODO: 
            var userName = claims.SingleOrDefault(c => c.Type == userNameClaimType)?.Value;
            var roles = claims.Where(c => c.Type == roleClaimType).Select(c => new Role(c.Value));

            if (string.IsNullOrEmpty(userName))
                throw MissingUserNameClaimException.New(userNameClaimType);

            var user = new User(userName, roles);

            _logger.LogDebug("User {userName} assigned roles: {roles}", userName, roles.Select(r => r.Name));

            return Task.FromResult(user);
        }
    }
}
