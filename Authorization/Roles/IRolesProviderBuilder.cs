using Authorization.Permissions;
using Authorization.Roles;

namespace Authorization.Tests.Sample
{
    public interface IRoleProviderBuilder
    {
        void ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions);
        void AddRole(Role role, params IPermission[] permissions);
        internal IRoleProvider Build();
    }
}