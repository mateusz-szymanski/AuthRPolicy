using AuthRPolicy.Core.Permissions;
using AuthRPolicy.MediatRExtensions;
using AuthRPolicy.Sample.Authorization.Permissions;
using AuthRPolicy.Sample.Domain.DocumentAggregate;
using MediatR;
using System;

namespace AuthRPolicy.Sample.Features.GetDocument
{
    public class GetDocumentQuery : IRequest<Document>, IAuthorizedRequest
    {
        public DocumentId DocumentId { get; }

        public GetDocumentQuery(Guid documentId)
        {
            DocumentId = new(documentId);
        }

        public PermissionAccessPolicy PermissionAccessPolicy => ListDocuments.AccessPolicy(new(DocumentId), new(DocumentId));
    }
}
