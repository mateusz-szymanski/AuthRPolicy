using AuthRAccessPolicy.Core.AccessPolicy;
using AuthRAccessPolicy.Core.Tests.Sample.Commands;

namespace AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentOwner
{
    public record DocumentOwnerAccessPolicy(DocumentId DocumentId) : IAccessPolicy
    {
        public string Name => "DocumentOwner";
    }
}