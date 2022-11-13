using AuthRPolicy.Core.AccessPolicy;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Core.TestAssemblies.ValidCheckers.Policy2
{
    public class AccessPolicy2Checker : IAccessPolicyChecker<AccessPolicy2>
    {
        public Task<bool> HasAccess(User user, AccessPolicy2 accessPolicy, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}