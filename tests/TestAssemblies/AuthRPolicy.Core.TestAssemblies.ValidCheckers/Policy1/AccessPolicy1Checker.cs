using AuthRPolicy.Core.AccessPolicy;

namespace AuthRPolicy.Core.TestAssemblies.ValidCheckers.Policy1
{
    public class AccessPolicy1Checker : IAccessPolicyChecker<AccessPolicy1>
    {
        public bool HasAccess(IUser user, AccessPolicy1 accessPolicy)
        {
            return true;
        }
    }
}