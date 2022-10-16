using AuthRAccessPolicy.Core.AccessPolicy;
using AuthRAccessPolicy.Core.Permissions;
using AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentOwner;
using AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentReviewer;
using System;

namespace AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentPermissions
{
    public static class ListDocuments
    {
        public const string MainName = "list-documents";

        public static IPermission AsOwner { get; } = new Permission<DocumentOwnerAccessPolicy>(MainName, "as-owner");
        public static IPermission AsReviwer { get; } = new Permission<DocumentReviewerAccessPolicy>(MainName, "as-reviewer");
        public static IPermission AsAdmin { get; } = new Permission<EmptyAccessPolicy>(MainName, "as-admin");

        public static Func<DocumentOwnerAccessPolicy, DocumentReviewerAccessPolicy, PermissionAccessPolicy> AccessPolicy =
            (documentOwner, documentReviewer) => new(MainName, documentOwner, documentReviewer, new EmptyAccessPolicy());
    }
}