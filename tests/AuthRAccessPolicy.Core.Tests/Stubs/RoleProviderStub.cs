using AuthRAccessPolicy.Core.Permissions;
using AuthRAccessPolicy.Core.Roles;

namespace AuthRAccessPolicy.Core.Tests.Stubs
{
    internal class RoleProviderStub : AbstractRoleProvider
    {
        public new RoleProviderStub AddRole(Role role, params IPermission[] permissions)
        {
            base.AddRole(role, permissions);

            return this;
        }
        public new RoleProviderStub ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions)
        {
            base.ConnectPermissions(permission, additionalPermissions);

            return this;
        }
    }
}
