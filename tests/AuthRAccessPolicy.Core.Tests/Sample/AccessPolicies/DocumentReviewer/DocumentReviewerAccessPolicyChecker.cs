using AuthRAccessPolicy.Core;
using AuthRAccessPolicy.Core.AccessPolicy;
using AuthRAccessPolicy.Core.Tests.Sample.Commands;

namespace AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentReviewer
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