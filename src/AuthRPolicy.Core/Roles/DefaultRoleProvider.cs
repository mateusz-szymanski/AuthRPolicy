using AuthRPolicy.Core.Exceptions;
using AuthRPolicy.Core.Roles;
using System.Collections.Generic;
using System.Linq;

namespace AuthRPolicy.Core.Permissions
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

        /// <summary>
        /// Connect role with permissions.
        /// </summary>
        /// <param name="role">Role.</param>
        /// <param name="permissions">Permissions assigned to the role.</param>
        /// <exception cref="RoleAlreadyAddedException">Given role is added for the second time.</exception>
        /// <returns>Builder</returns>
        public IDefaultRoleProviderBuilder AddRole(Role role, params IPermission[] permissions)
        {
            if (_roleToPermissions.ContainsKey(role))
                throw RoleAlreadyAddedException.New(role);

            _roleToPermissions.Add(role, permissions.ToHashSet());

            return this;
        }


        /// <summary>
        /// Connect permissions. Every role that contains given permission will also have additionalPermissions.
        /// </summary>
        /// <param name="permission">Permission.</param>
        /// <param name="permissionsToBeAdded">Additional permissions for given permission.</param>
        /// <exception cref="PermissionAlreadyConnectedException">Given permission is added for the second time.</exception>
        /// <summary>
        /// <returns>Builder</returns>
        public IDefaultRoleProviderBuilder ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions)
        {
            if (_permissionToAdditionalPermissions.ContainsKey(permission))
                throw PermissionAlreadyConnectedException.New(permission);

            _permissionToAdditionalPermissions.Add(permission, additionalPermissions.ToHashSet());

            return this;
        }

        /// <summary>
        /// Get all permissions for given role:
        /// <list type="number">	
        ///   <item>Permissions connected directly to the role.</item>
        ///   <item>Additional permissions connected with them.</item>
        /// </list>
        /// All additional permissions are added recusively.
        /// </summary>
        /// <param name="role">Role.</param>
        /// <returns>List of permissions.</returns>
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

        /// <summary>
        /// Get list of all added roles.
        /// </summary>
        /// <returns>List of roles.</returns>
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