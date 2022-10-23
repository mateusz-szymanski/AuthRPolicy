namespace AuthRPolicy.Core.AccessPolicy
{
    internal class EmptyAccessPolicyChecker : IAccessPolicyChecker<EmptyAccessPolicy>
    {
        public bool HasAccess(User user, EmptyAccessPolicy accessPolicy)
        {
            return true;
        }
    }
}