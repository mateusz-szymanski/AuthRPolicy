﻿using Authorization.AccessPolicy;
using Authorization.Permissions;
using System;

namespace Authorization.Tests.Sample.DocumentPermissions
{
    public static class CreateDocument
    {
        public const string MainName = "create-document";

        public static IPermission Default { get; } = new Permission<EmptyAccessPolicy>(MainName);

        public static Func<PermissionAccessPolicy> AccessPolicy = () => new(MainName, new EmptyAccessPolicy());
    }
}