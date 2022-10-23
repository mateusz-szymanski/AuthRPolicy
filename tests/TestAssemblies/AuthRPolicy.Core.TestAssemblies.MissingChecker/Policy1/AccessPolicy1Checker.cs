using AuthRPolicy.Core.AccessPolicy;

namespace AuthRPolicy.Core.TestAssemblies.MissingChecker.Policy1
{
    public class AccessPolicy1Checker : IAccessPolicyChecker<AccessPolicy1>
    {
        public bool HasAccess(User user, AccessPolicy1 accessPolicy)
        {
            return true;
        }
    }
}