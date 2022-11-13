using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Core.AccessPolicy
{
    public interface IAccessPolicyChecker<in TAccessPolicy>
        where TAccessPolicy : IAccessPolicy
    {
        Task<bool> HasAccess(User user, TAccessPolicy accessPolicy, CancellationToken cancellationToken);
    }
}