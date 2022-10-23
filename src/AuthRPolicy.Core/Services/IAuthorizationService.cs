using AuthRPolicy.Core.Permissions;
using System.Collections.Generic;

namespace AuthRPolicy.Core.Services
{
    public interface IAuthorizationService
    {
        bool IsUserAuthorized(User user, PermissionAccessPolicy permissionAccessPolicy);
        IEnumerable<IPermission> GetUserPermissions(User user);
    }
}