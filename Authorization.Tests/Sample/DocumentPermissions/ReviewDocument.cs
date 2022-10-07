using Authorization.Permissions;
using Authorization.Tests.Sample.DocumentReviewer;
using System;

namespace Authorization.Tests.Sample.DocumentPermissions
{
    public static class ReviewDocument
    {
        public const string MainName = "review-document";

        public static IPermission Default { get; } = new Permission<DocumentReviewerAccessPolicy>(MainName);

        public static Func<DocumentReviewerAccessPolicy, PermissionAccessPolicy> AccessPolicy =
            (documentReviewerAccessPolicy) => new(MainName, documentReviewerAccessPolicy);
    }
}