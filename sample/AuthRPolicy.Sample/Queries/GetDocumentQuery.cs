using AuthRPolicy.Core.Permissions;
using AuthRPolicy.MediatRExtensions;
using AuthRPolicy.Sample.Authorization.Permissions;
using AuthRPolicy.Sample.Commands;
using MediatR;

namespace AuthRPolicy.Sample.Queries
{
    public class GetDocumentQuery : IRequest<Document>, IAuthorizedRequest
    {
        public DocumentId DocumentId { get; }

        public GetDocumentQuery(int documentId)
        {
            DocumentId = new(documentId);
        }

        public PermissionAccessPolicy PermissionAccessPolicy => ListDocuments.AccessPolicy(new(DocumentId), new(DocumentId));
    }
}
