using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Sample.Commands;

namespace AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentReviewer
{
    public record DocumentReviewerAccessPolicy(DocumentId DocumentId) : IAccessPolicy
    {
        public string Name => "DocumentReviewer";
    }
}