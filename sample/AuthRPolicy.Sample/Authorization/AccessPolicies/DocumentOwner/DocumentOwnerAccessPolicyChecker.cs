using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Sample.Commands;

namespace AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentOwner
{
    public class DocumentOwnerAccessPolicyChecker : IAccessPolicyChecker<DocumentOwnerAccessPolicy>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentOwnerAccessPolicyChecker(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public bool HasAccess(IUser user, DocumentOwnerAccessPolicy accessPolicy)
        {
            var documentOwner = _documentRepository.GetDocumentOwner(accessPolicy.DocumentId);
            return documentOwner == user.UserName;
        }
    }
}