using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Tests.Sample.Commands;

namespace AuthRPolicy.Core.Tests.Sample.AccessPolicies.DocumentReviewer
{
    public record DocumentReviewerAccessPolicy(DocumentId DocumentId) : IAccessPolicy
    {
        public string Name => "DocumentReviewer";
    }
}