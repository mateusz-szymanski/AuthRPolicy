using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Sample.Commands;

namespace AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentOwner
{
    public record DocumentOwnerAccessPolicy(DocumentId DocumentId) : IAccessPolicy
    {
        public string Name => "DocumentOwner";
    }
}