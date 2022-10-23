using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Roles.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AuthRPolicy.Core.Roles
{
    public class DefaultRoleProvider : IRoleProvider, IDefaultRoleProviderBuilder
    {
        private readonly Dictionary<Role, HashSet<IPermission>> _roleToPermissions;
        private readonly Dictionary<IPermission, HashSet<IPermission>> _permissionToAdditionalPermissions;

        public DefaultRoleProvider()
        {
            _roleToPermissions = new();
            _permissionToAdditionalPermissions = new();
        }

        public IDefaultRoleProviderBuilder AddRole(Role role, params IPermission[] permissions)
        {
            if (_roleToPermissions.ContainsKey(role))
                throw RoleAlreadyAddedException.New(role);

            _roleToPermissions.Add(role, permissions.ToHashSet());

            return this;
        }

        public IDefaultRoleProviderBuilder ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions)
        {
            if (_permissionToAdditionalPermissions.ContainsKey(permission))
                throw PermissionAlreadyConnectedException.New(permission);

            _permissionToAdditionalPermissions.Add(permission, additionalPermissions.ToHashSet());

            return this;
        }

        public IEnumerable<IPermission> GetPermissionsForRole(Role role)
        {
            var permissionsAddedToRole = _roleToPermissions.GetValueOrDefault(role) ?? new();
            var permissionsToIterate = new Stack<IPermission>(permissionsAddedToRole);

            var allAssignedPermissions = new HashSet<IPermission>();
            while (permissionsToIterate.Any())
            {
                var permission = permissionsToIterate.Pop();
                allAssignedPermissions.Add(permission);

                var additionalPermissions = _permissionToAdditionalPermissions.GetValueOrDefault(permission) ?? new();
                foreach (var additionalPermission in additionalPermissions)
                {
                    if (allAssignedPermissions.Contains(additionalPermission))
                        continue;

                    permissionsToIterate.Push(additionalPermission);
                }
            }

            return allAssignedPermissions;
        }

        public IEnumerable<Role> GetAvailableRoles()
        {
            return _roleToPermissions.Keys;
        }

        public IRoleProvider Build()
        {
            return this;
        }
    }
}