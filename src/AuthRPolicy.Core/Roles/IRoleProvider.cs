using AuthRPolicy.Core.Permissions;
using System.Collections.Generic;

namespace AuthRPolicy.Core.Roles
{
    public interface IRoleProvider
    {
        IEnumerable<Role> GetAvailableRoles();
        IEnumerable<IPermission> GetPermissionsForRole(Role role);
    }
}