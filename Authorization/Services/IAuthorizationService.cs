using Authorization;
using Authorization.AccessPolicy;

namespace Authorization.Services
{
    public interface IAuthorizationService
    {
        bool IsUserAuthorized(IUser user, PermissionAccessPolicy permissionAccessPolicy);
    }
}