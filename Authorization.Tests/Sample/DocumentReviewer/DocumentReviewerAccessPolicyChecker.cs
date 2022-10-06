using Authorization.AccessPolicy;
using Authorization.Tests.Sample.Commands;

namespace Authorization.Tests.Sample.DocumentReviewer
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