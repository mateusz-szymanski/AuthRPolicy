using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Core.AccessPolicy
{
    internal class EmptyAccessPolicyChecker : IAccessPolicyChecker<EmptyAccessPolicy>
    {
        public Task<bool> HasAccess(User user, EmptyAccessPolicy accessPolicy, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}