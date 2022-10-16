using AuthRPolicy.Core.Permissions;

namespace AuthRPolicy.Core.Roles
{
    public interface IDefaultRoleProviderBuilder
    {
        IDefaultRoleProviderBuilder ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions);
        IDefaultRoleProviderBuilder AddRole(Role role, params IPermission[] permissions);
        internal IRoleProvider Build();
    }
}