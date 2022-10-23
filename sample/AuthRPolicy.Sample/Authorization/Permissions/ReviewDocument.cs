using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Sample.Authorization.AccessPolicies.DocumentReviewer;
using System;

namespace AuthRPolicy.Sample.Authorization.Permissions
{
    public static class ReviewDocument
    {
        public const string MainName = "review-document";

        public static IPermission Default { get; } = new Permission<DocumentReviewerAccessPolicy>(MainName);

        public readonly static Func<DocumentReviewerAccessPolicy, PermissionAccessPolicy> AccessPolicy =
            (documentReviewerAccessPolicy) => new(MainName, documentReviewerAccessPolicy);
    }
}