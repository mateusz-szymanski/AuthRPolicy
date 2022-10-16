using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Tests.Sample.Commands;

namespace AuthRPolicy.Core.Tests.Sample.AccessPolicies.DocumentReviewer
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