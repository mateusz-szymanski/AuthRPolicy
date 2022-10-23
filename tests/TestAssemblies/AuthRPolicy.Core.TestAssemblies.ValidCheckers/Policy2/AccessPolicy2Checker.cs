using AuthRPolicy.Core.AccessPolicy;

namespace AuthRPolicy.Core.TestAssemblies.ValidCheckers.Policy2
{
    public class AccessPolicy2Checker : IAccessPolicyChecker<AccessPolicy2>
    {
        public bool HasAccess(IUser user, AccessPolicy2 accessPolicy)
        {
            return true;
        }
    }
}