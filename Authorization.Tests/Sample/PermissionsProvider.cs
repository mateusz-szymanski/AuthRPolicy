using Authorization.Permissions;
using Authorization.Roles;
using System.Collections.Generic;

namespace Authorization.Tests.Sample
{
    public class PermissionsProvider : IPermissionProvider
    {
        private readonly Dictionary<Role, HashSet<Permission>> _roleToPermissions;
        private readonly Dictionary<Permission, HashSet<Permission>> _permissionToImplicitlyAddedPermissions;

        public PermissionsProvider()
        {
            _roleToPermissions = new();
            _permissionToImplicitlyAddedPermissions = new();

            _roleToPermissions.Add(Roles.DocumentCreator, new() { DocumentPermissions.CreateDocument });
            _roleToPermissions.Add(Roles.DocumentReader, new() { DocumentPermissions.ListDocuments });

            _permissionToImplicitlyAddedPermissions.Add(DocumentPermissions.CreateDocument, new() { DocumentPermissions.ListDocuments });
        }

        // TODO: Move to common code, maybe split into get methods per dictionary?
        public IEnumerable<Permission> GetPermissionsForRole(Role role)
        {
            var explicitPermissions = _roleToPermissions.GetValueOrDefault(role) ?? new();

            var allAssignedPermissions = new HashSet<Permission>();
            foreach (var explicitPermission in explicitPermissions)
            {
                allAssignedPermissions.Add(explicitPermission);

                var implicitlyAddedPermissions = _permissionToImplicitlyAddedPermissions.GetValueOrDefault(explicitPermission) ?? new();
                foreach (var implicitlyAddedPermission in implicitlyAddedPermissions)
                {
                    allAssignedPermissions.Add(implicitlyAddedPermission);
                }
            }

            return allAssignedPermissions;
        }
    }
}