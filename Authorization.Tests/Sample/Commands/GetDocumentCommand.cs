using Authorization.AccessPolicy;
using Authorization.MediatRExtensions;

namespace Authorization.Tests.Sample.Commands
{
    public class GetDocumentCommand : IAuthorizedRequest
    {
        public DocumentId DocumentId { get; }

        public GetDocumentCommand(int documentId)
        {
            DocumentId = new(documentId);
        }

        public PermissionAccessPolicy PermissionAccessPolicy => DocumentPermissionsAccessPolicy.ListDocuments(new(DocumentId), new(DocumentId));
    }
}
