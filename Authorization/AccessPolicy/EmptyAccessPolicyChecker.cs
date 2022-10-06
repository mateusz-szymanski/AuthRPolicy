namespace Authorization.AccessPolicy
{
    public class EmptyAccessPolicyChecker : IAccessPolicyChecker<EmptyAccessPolicy>
    {
        public bool HasAccess(IUser user, EmptyAccessPolicy accessPolicy)
        {
            return true;
        }
    }
}