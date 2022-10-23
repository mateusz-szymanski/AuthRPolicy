using AuthRPolicy.Core;
using AuthRPolicy.Core.Roles;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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

            // TODO: 
            var userName = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => new Role(c.Value));

            var user = new User(userName, roles);

            return Task.FromResult(user);
        }
    }
}
