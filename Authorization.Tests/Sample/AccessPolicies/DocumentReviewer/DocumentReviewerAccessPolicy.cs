using Authorization.AccessPolicy;
using Authorization.Tests.Sample.Commands;

namespace Authorization.Tests.Sample.AccessPolicies.DocumentReviewer
{
    public record DocumentReviewerAccessPolicy(DocumentId DocumentId) : IAccessPolicy
    {
        public string Name => "DocumentReviewer";
    }
}