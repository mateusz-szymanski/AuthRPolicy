using AuthRAccessPolicy.Core.Permissions;
using AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentReviewer;
using System;

namespace AuthRAccessPolicy.Core.Tests.Sample.AccessPolicies.DocumentPermissions
{
    public static class ReviewDocument
    {
        public const string MainName = "review-document";

        public static IPermission Default { get; } = new Permission<DocumentReviewerAccessPolicy>(MainName);

        public static Func<DocumentReviewerAccessPolicy, PermissionAccessPolicy> AccessPolicy =
            (documentReviewerAccessPolicy) => new(MainName, documentReviewerAccessPolicy);
    }
}