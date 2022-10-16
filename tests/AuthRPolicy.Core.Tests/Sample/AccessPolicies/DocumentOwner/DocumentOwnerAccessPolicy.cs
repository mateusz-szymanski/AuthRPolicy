using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Tests.Sample.Commands;

namespace AuthRPolicy.Core.Tests.Sample.AccessPolicies.DocumentOwner
{
    public record DocumentOwnerAccessPolicy(DocumentId DocumentId) : IAccessPolicy
    {
        public string Name => "DocumentOwner";
    }
}