namespace AuthRPolicy.Core.AccessPolicy
{
    public interface IAccessPolicyChecker<in TAccessPolicy>
        where TAccessPolicy : IAccessPolicy
    {
        bool HasAccess(IUser user, TAccessPolicy accessPolicy);
    }
}