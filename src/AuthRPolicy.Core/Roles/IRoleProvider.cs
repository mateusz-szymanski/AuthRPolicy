using AuthRPolicy.Core.Permissions;
using System.Collections.Generic;

namespace AuthRPolicy.Core.Roles
{
    // TODO: consider async api
    public interface IRoleProvider
    {
        /// <summary>
        /// Get list of all added roles.
        /// </summary>
        /// <returns>List of roles.</returns>
        IEnumerable<Role> GetAvailableRoles();

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
        IEnumerable<IPermission> GetPermissionsForRole(Role role);
    }
}