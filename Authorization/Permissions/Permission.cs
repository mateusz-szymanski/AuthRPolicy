﻿using Authorization.AccessPolicy;
using Authorization.Exceptions;
using System;
using System.Diagnostics;

namespace Authorization.Permissions
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public record Permission<TAccessPolicy> : IPermission
        where TAccessPolicy : IAccessPolicy
    {
        public Type AccessPolicyType { get; }
        public string MainName { get; }
        public string SubName { get; }
        public string FullName { get; }

        public Permission(string mainName, string subName = "")
        {
            if (string.IsNullOrWhiteSpace(mainName))
                throw MissingPermissionNameException.New(mainName);

            AccessPolicyType = typeof(TAccessPolicy);
            MainName = mainName;
            SubName = subName;

            FullName = string.IsNullOrWhiteSpace(SubName) ? MainName : $"{MainName}.{SubName}";
        }

        public bool HasMainNameEqualTo(string mainPermissionName)
        {
            return MainName == mainPermissionName;
        }

        private string GetDebuggerDisplay()
        {
            return $"{FullName} - {AccessPolicyType.Name}";
        }
    }
}