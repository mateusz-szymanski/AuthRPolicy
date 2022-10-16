using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Permissions;
using System;

namespace AuthRPolicy.Core.Tests.Sample.AccessPolicies.DocumentPermissions
{
    public static class CreateDocument
    {
        public const string MainName = "create-document";

        public static IPermission Default { get; } = new Permission<EmptyAccessPolicy>(MainName);

        public static Func<PermissionAccessPolicy> AccessPolicy = () => new(MainName, new EmptyAccessPolicy());
    }
}