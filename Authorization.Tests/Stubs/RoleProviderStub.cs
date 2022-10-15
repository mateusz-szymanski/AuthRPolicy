using Authorization.Permissions;
using Authorization.Roles;

namespace Authorization.Tests.Stubs
{
    internal class RoleProviderStub : AbstractRoleProvider
    {
        public new RoleProviderStub AddRole(Role role, params IPermission[] permissions)
        {
            base.AddRole(role, permissions);

            return this;
        }
    }
}
