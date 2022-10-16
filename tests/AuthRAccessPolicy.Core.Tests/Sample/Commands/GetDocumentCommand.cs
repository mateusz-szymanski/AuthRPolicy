using AuthRAccessPolicy.Core.MediatRExtensions;
using AuthRAccessPolicy.Core.Permissions;
using AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentPermissions;

namespace AuthRAccessPolicy.Core.Tests.Sample.Commands
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
