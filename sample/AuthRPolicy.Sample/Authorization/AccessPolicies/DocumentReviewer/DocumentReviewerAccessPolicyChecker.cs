using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Sample.Domain.DocumentAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentReviewer
{
    public class DocumentReviewerAccessPolicyChecker : IAccessPolicyChecker<DocumentReviewerAccessPolicy>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentReviewerAccessPolicyChecker(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<bool> HasAccess(User user, DocumentReviewerAccessPolicy accessPolicy, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetDocument(accessPolicy.DocumentId, cancellationToken);
            var hasAccess = document.Reviewer == user.UserName;

            return hasAccess;
        }
    }
}