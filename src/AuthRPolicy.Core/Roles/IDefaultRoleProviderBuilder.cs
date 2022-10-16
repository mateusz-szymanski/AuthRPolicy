using AuthRPolicy.Core.Permissions;

namespace AuthRPolicy.Core.Roles
{
    public interface IDefaultRoleProviderBuilder
    {
        void ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions);
        void AddRole(Role role, params IPermission[] permissions);
        internal IRoleProvider Build();
    }
}