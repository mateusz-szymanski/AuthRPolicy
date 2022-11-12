namespace AuthRPolicy.Core.AccessPolicy
{
    // TODO: async api
    public interface IAccessPolicyChecker<in TAccessPolicy>
        where TAccessPolicy : IAccessPolicy
    {
        bool HasAccess(User user, TAccessPolicy accessPolicy);
    }
}