using AuthRPolicy.Core;

namespace AuthRPolicy.MediatRExtensions.Services
{
    public interface ICurrentUserService
    {
        Task<IUser> GetCurrentUser(CancellationToken cancellationToken);
    }
}
