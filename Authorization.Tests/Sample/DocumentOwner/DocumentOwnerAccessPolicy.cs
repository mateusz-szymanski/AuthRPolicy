using Authorization.AccessPolicy;
using Authorization.Tests.Sample.Commands;

namespace Authorization.Tests.Sample.DocumentOwner
{
    public record DocumentOwnerAccessPolicy(DocumentId DocumentId) : IAccessPolicy
    {
        public string Name => "DocumentOwner";
    }
}