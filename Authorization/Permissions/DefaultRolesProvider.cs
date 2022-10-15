using Authorization.Permissions;
using Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Authorization.Tests.Sample
{
    public abstract class DefaultRoleProvider : IRoleProvider
    {
        private readonly Dictionary<Role, HashSet<IPermission>> _roleToPermissions;
        private readonly Dictionary<IPermission, HashSet<IPermission>> _permissionToAdditionalPermissions;

        public DefaultRoleProvider()
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
            var explicitPermissions = _roleToPermissions.GetValueOrDefault(role) ?? new();

            var allAssignedPermissions = new HashSet<IPermission>();
            foreach (var explicitPermission in explicitPermissions)
            {
                allAssignedPermissions.Add(explicitPermission);

                var implicitlyAddedPermissions = _permissionToAdditionalPermissions.GetValueOrDefault(explicitPermission) ?? new();
                foreach (var implicitlyAddedPermission in implicitlyAddedPermissions)
                {
                    allAssignedPermissions.Add(implicitlyAddedPermission);
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