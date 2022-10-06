using Authorization.AccessPolicy;
using Authorization.Tests.Sample.DocumentOwner;
using Authorization.Tests.Sample.DocumentReviewer;
using System;

namespace Authorization.Tests.Sample
{
    public static class DocumentPermissionsAccessPolicy
    {
        public static Func<DocumentOwnerAccessPolicy, DocumentReviewerAccessPolicy, PermissionAccessPolicy> ListDocuments =
            (documentOwner, documentReviewer) => new(DocumentPermissions.ListDocuments, documentOwner, documentReviewer, new EmptyAccessPolicy());

        public static Func<PermissionAccessPolicy> CreateProduct =
            () => new(DocumentPermissions.CreateDocument, new EmptyAccessPolicy());
    }
}