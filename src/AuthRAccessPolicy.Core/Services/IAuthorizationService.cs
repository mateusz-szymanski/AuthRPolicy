using AuthRAccessPolicy.Core.Permissions;
using System.Collections.Generic;

namespace AuthRAccessPolicy.Core.Services
{
    public interface IAuthorizationService
    {
        bool IsUserAuthorized(IUser user, PermissionAccessPolicy permissionAccessPolicy);
        IEnumerable<IPermission> GetUserPermissions(IUser user);
    }
}