using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Permissions;
using System;

namespace AuthRPolicy.Sample.Authorization.Permissions
{
    public static class CreateDocument
    {
        public const string MainName = "create-document";

        public static IPermission Default { get; } = new Permission<EmptyAccessPolicy>(MainName);

        public readonly static Func<PermissionAccessPolicy> AccessPolicy = () => new(MainName, new EmptyAccessPolicy());
    }
}