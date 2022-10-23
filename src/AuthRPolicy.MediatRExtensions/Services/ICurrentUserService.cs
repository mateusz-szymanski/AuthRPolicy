using AuthRPolicy.Core;

namespace AuthRPolicy.MediatRExtensions.Services
{
    public interface ICurrentUserService
    {
        Task<User> GetCurrentUser(CancellationToken cancellationToken);
    }
}
