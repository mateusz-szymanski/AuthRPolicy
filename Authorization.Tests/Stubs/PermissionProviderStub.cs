using Authorization.Permissions;
using Authorization.Roles;
using System.Collections.Generic;
using System.Linq;

namespace Authorization.Tests.Stubs
{
    internal class PermissionProviderStub : IPermissionProvider
    {
        private readonly Dictionary<Role, IEnumerable<IPermission>> _roleToPermissions;

        public PermissionProviderStub()
        {
            _roleToPermissions = new();
        }

        public PermissionProviderStub WithRole(Role role, params IPermission[] permissions)
        {
            _roleToPermissions.Add(role, permissions);
            return this;
        }

        public IEnumerable<IPermission> GetPermissionsForRole(Role role)
        {
            return _roleToPermissions.GetValueOrDefault(role) ?? Enumerable.Empty<IPermission>();
        }
    }
}
