using Authorization.Permissions;
using Authorization.Roles;

namespace Authorization.Tests.Sample
{
    class DefaultRoleProviderBuilder : DefaultRoleProvider, IRoleProviderBuilder
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