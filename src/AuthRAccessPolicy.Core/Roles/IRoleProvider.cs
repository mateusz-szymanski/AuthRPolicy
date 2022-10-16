using AuthRAccessPolicy.Core.Permissions;
using System.Collections.Generic;

namespace AuthRAccessPolicy.Core.Roles
{
    public interface IRoleProvider
    {
        IEnumerable<Role> GetAvailableRoles();
        IEnumerable<IPermission> GetPermissionsForRole(Role role);
    }
}