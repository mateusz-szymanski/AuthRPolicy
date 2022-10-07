using Authorization;
using Authorization.Permissions;

namespace Authorization.Services
{
    public interface IAuthorizationService
    {
        bool IsUserAuthorized(IUser user, PermissionAccessPolicy permissionAccessPolicy);
    }
}