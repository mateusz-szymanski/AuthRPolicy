namespace AuthRPolicy.Core.AccessPolicy
{
    internal class EmptyAccessPolicyChecker : IAccessPolicyChecker<EmptyAccessPolicy>
    {
        public bool HasAccess(IUser user, EmptyAccessPolicy accessPolicy)
        {
            return true;
        }
    }
}