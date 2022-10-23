namespace AuthRPolicy.Core.AccessPolicy
{
    public interface IAccessPolicyChecker<in TAccessPolicy>
        where TAccessPolicy : IAccessPolicy
    {
        bool HasAccess(User user, TAccessPolicy accessPolicy);
    }
}