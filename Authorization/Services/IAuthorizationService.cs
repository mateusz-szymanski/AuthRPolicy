using Authorization.Permissions;
using System.Collections;
using System.Collections.Generic;

namespace Authorization.Services
{
    public interface IAuthorizationService
    {
        bool IsUserAuthorized(IUser user, PermissionAccessPolicy permissionAccessPolicy);
        IEnumerable<IPermission> GetUserPermissions(IUser user);
    }
}