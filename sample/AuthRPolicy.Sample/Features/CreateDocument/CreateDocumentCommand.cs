using AuthRPolicy.Core.Permissions;
using AuthRPolicy.MediatRExtensions;
using MediatR;

namespace AuthRPolicy.Sample.Features.CreateDocument
{
    public class CreateDocumentCommand : IRequest, IAuthorizedRequest
    {
        public string Title { get; private set; }
        public string Reviewer { get; private set; }

        public CreateDocumentCommand(string title, string reviewer)
        {
            Title = title;
            Reviewer = reviewer;
        }

        public PermissionAccessPolicy PermissionAccessPolicy => Authorization.Permissions.CreateDocument.AccessPolicy();
    }
}
