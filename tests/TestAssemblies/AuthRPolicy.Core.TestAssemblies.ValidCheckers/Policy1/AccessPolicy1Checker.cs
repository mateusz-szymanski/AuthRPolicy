using AuthRPolicy.Core.AccessPolicy;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Core.TestAssemblies.ValidCheckers.Policy1
{
    public class AccessPolicy1Checker : IAccessPolicyChecker<AccessPolicy1>
    {
        public Task<bool> HasAccess(User user, AccessPolicy1 accessPolicy, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}