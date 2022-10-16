using AuthRAccessPolicy.Core.AccessPolicy;
using AuthRAccessPolicy.Core.Tests.Sample.Commands;

namespace AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentReviewer
{
    public record DocumentReviewerAccessPolicy(DocumentId DocumentId) : IAccessPolicy
    {
        public string Name => "DocumentReviewer";
    }
}