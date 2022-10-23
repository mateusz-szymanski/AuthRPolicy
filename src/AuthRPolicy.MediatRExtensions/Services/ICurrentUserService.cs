using AuthRPolicy.Core;

namespace AuthRPolicy.MediatRExtensions.Services
{
    public interface ICurrentUserService
    {
        Task<Core.User> GetCurrentUser(CancellationToken cancellationToken);
    }
}
