using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Sample.Domain.DocumentAggregate;

namespace AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentOwner
{
    public class DocumentOwnerAccessPolicyChecker : IAccessPolicyChecker<DocumentOwnerAccessPolicy>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentOwnerAccessPolicyChecker(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public bool HasAccess(User user, DocumentOwnerAccessPolicy accessPolicy)
        {
            var document = _documentRepository.GetDocument(accessPolicy.DocumentId);
            return document.Owner == user.UserName;
        }
    }
}