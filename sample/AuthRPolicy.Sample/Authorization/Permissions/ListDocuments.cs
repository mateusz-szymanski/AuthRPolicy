using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentOwner;
using AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentReviewer;
using System;

namespace AuthRPolicy.Sample.Authorization.Permissions
{
    public static class ListDocuments
    {
        public const string MainName = "list-documents";

        public static IPermission AsOwner { get; } = new Permission<DocumentOwnerAccessPolicy>(MainName, "as-owner");
        public static IPermission AsReviwer { get; } = new Permission<DocumentReviewerAccessPolicy>(MainName, "as-reviewer");
        public static IPermission AsAdmin { get; } = new Permission<EmptyAccessPolicy>(MainName, "as-admin");

        public readonly static Func<DocumentOwnerAccessPolicy, DocumentReviewerAccessPolicy, PermissionAccessPolicy> AccessPolicy =
            (documentOwner, documentReviewer) => new(MainName, documentOwner, documentReviewer, new EmptyAccessPolicy());
    }
}