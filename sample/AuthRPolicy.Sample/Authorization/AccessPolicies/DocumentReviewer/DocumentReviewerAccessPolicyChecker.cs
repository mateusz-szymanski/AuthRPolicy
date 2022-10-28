using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Sample.Domain.DocumentAggregate;

namespace AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentReviewer
{
    public class DocumentReviewerAccessPolicyChecker : IAccessPolicyChecker<DocumentReviewerAccessPolicy>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentReviewerAccessPolicyChecker(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public bool HasAccess(User user, DocumentReviewerAccessPolicy accessPolicy)
        {
            var document = _documentRepository.GetDocument(accessPolicy.DocumentId);
            return document.Reviewer == user.UserName;
        }
    }
}