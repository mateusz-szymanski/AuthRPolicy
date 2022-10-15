﻿using Authorization.Roles;

namespace Authorization.Permissions
{
    class DefaultRoleProviderBuilder : AbstractRoleProvider, IRoleProviderBuilder
    {
        void IRoleProviderBuilder.AddRole(Role role, params IPermission[] permissions)
        {
            AddRole(role, permissions);
        }

        void IRoleProviderBuilder.ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions)
        {
            ConnectPermissions(permission, additionalPermissions);
        }
    }
}