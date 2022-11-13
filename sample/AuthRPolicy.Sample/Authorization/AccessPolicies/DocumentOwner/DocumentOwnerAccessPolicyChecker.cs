using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Sample.Domain.DocumentAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentOwner
{
    public class DocumentOwnerAccessPolicyChecker : IAccessPolicyChecker<DocumentOwnerAccessPolicy>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentOwnerAccessPolicyChecker(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<bool> HasAccess(User user, DocumentOwnerAccessPolicy accessPolicy, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetDocument(accessPolicy.DocumentId, cancellationToken);
            var hasAccess = document.Owner == user.UserName;

            return hasAccess;
        }
    }
}