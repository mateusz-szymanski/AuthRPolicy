using AuthRPolicy.Core.Permissions;
using System.Collections.Generic;

namespace AuthRPolicy.Core.Roles
{
    public interface IRoleProvider
    {
        // TODO: consider async api
        IEnumerable<Role> GetAvailableRoles();
        IEnumerable<IPermission> GetPermissionsForRole(Role role);
    }
}