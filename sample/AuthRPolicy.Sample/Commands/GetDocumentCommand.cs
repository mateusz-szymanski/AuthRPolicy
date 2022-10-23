using AuthRPolicy.Core.Permissions;
using AuthRPolicy.MediatRExtensions;
using AuthRPolicy.Sample.Authorization.Permissions;

namespace AuthRPolicy.Sample.Commands
{
    public class GetDocumentCommand : IAuthorizedRequest
    {
        public DocumentId DocumentId { get; }

        public GetDocumentCommand(int documentId)
        {
            DocumentId = new(documentId);
        }

        public PermissionAccessPolicy PermissionAccessPolicy => ListDocuments.AccessPolicy(new(DocumentId), new(DocumentId));
    }
}
