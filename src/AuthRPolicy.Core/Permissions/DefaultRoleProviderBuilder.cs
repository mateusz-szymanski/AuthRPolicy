using AuthRPolicy.Core.Roles;

namespace AuthRPolicy.Core.Permissions
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