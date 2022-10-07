using Authorization.Permissions;
using Authorization.Tests.Sample.AccessPolicies.DocumentReviewer;
using System;

namespace Authorization.Tests.Sample.AccessPolicies.DocumentPermissions
{
    public static class ReviewDocument
    {
        public const string MainName = "review-document";

        public static IPermission Default { get; } = new Permission<DocumentReviewerAccessPolicy>(MainName);

        public static Func<DocumentReviewerAccessPolicy, PermissionAccessPolicy> AccessPolicy =
            (documentReviewerAccessPolicy) => new(MainName, documentReviewerAccessPolicy);
    }
}