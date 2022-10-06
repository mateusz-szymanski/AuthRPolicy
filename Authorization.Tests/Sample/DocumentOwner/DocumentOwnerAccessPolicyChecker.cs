using Authorization.AccessPolicy;
using Authorization.Tests.Sample.Commands;

namespace Authorization.Tests.Sample.DocumentOwner
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