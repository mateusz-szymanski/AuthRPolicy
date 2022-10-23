using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Sample.Commands;

namespace AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentReviewer
{
    public class DocumentReviewerAccessPolicyChecker : IAccessPolicyChecker<DocumentReviewerAccessPolicy>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentReviewerAccessPolicyChecker(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public bool HasAccess(IUser user, DocumentReviewerAccessPolicy accessPolicy)
        {
            var documentReviewer = _documentRepository.GetDocumentReviewer(accessPolicy.DocumentId);
            return documentReviewer == user.UserName;
        }
    }
}