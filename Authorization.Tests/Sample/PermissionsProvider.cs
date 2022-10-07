using Authorization.Permissions;
using Authorization.Roles;
using Authorization.Tests.Sample.AccessPolicies.DocumentPermissions;
using System.Collections.Generic;

namespace Authorization.Tests.Sample
{
    public class PermissionsProvider : IPermissionProvider
    {
        private readonly Dictionary<Role, HashSet<IPermission>> _roleToPermissions;
        private readonly Dictionary<IPermission, HashSet<IPermission>> _permissionToImplicitlyAddedPermissions;

        public PermissionsProvider()
        {
            _roleToPermissions = new();
            _permissionToImplicitlyAddedPermissions = new();

            _roleToPermissions.Add(Roles.DocumentCreator, new() { CreateDocument.Default });
            _roleToPermissions.Add(Roles.DocumentReviewer, new() { ListDocuments.AsReviwer });
            _roleToPermissions.Add(Roles.Admin, new() { ListDocuments.AsAdmin });

            _permissionToImplicitlyAddedPermissions.Add(CreateDocument.Default, new() { ListDocuments.AsOwner });
        }

        // TODO: Move to common code, maybe split into get methods per dictionary?
        public IEnumerable<IPermission> GetPermissionsForRole(Role role)
        {
            var explicitPermissions = _roleToPermissions.GetValueOrDefault(role) ?? new();

            var allAssignedPermissions = new HashSet<IPermission>();
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