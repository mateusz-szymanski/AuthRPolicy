using Authorization.Roles;
using System.Collections.Generic;

namespace Authorization.Permissions
{
    public interface IPermissionProvider
    {
        IEnumerable<Permission> GetPermissionsForRole(Role role);
    }
}