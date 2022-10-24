using AuthRPolicy.Core;
using AuthRPolicy.Core.Roles;
using Microsoft.AspNetCore.Http;

namespace AuthRPolicy.MediatRExtensions.Services
{
    internal class HttpContextBasedCurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextBasedCurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
            {
                throw new Exception(); // TODO:
            }

            var user = new User(userName, roles);

            return Task.FromResult(user);
        }
    }
}
