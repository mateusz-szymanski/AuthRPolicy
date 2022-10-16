using Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Authorization.Permissions
{
    public abstract class AbstractRoleProvider : IRoleProvider
    {
        private readonly Dictionary<Role, HashSet<IPermission>> _roleToPermissions;
        private readonly Dictionary<IPermission, HashSet<IPermission>> _permissionToAdditionalPermissions;

        public AbstractRoleProvider()
        {
            _roleToPermissions = new();
            _permissionToAdditionalPermissions = new();
        }

        /// <summary>
        /// Connect role with permissions
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permissions"></param>
        /// <exception cref="Exception"></exception>
        protected void AddRole(Role role, params IPermission[] permissions)
        {
            if (_roleToPermissions.ContainsKey(role))
                throw new Exception($"Role {role.Name} already added"); // TODO: custom exception

            _roleToPermissions.Add(role, permissions.ToHashSet());
        }

        /// <summary>
        /// Connect permissions. Every role that contains permission will also have additionalPermissions
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="permissionsToBeAdded"></param>
        /// <exception cref="Exception"></exception>
        protected void ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions)
        {
            if (_permissionToAdditionalPermissions.ContainsKey(permission))
                throw new Exception($"Permission {permission.FullName} already added"); // TODO: custom exception

            _permissionToAdditionalPermissions.Add(permission, additionalPermissions.ToHashSet());
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