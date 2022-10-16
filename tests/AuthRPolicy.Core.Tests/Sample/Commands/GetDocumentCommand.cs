using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Tests.Sample.AccessPolicies.DocumentPermissions;
using AuthRPolicy.MediatRExtensions;

namespace AuthRPolicy.Core.Tests.Sample.Commands
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
