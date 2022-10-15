using Authorization.Permissions;

namespace Authorization.Roles
{
    public interface IRoleProviderBuilder
    {
        void ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions);
        void AddRole(Role role, params IPermission[] permissions);
        internal IRoleProvider Build();
    }
}