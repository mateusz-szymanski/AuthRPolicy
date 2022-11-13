using AuthRPolicy.Core.Permissions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Core.Services
{
    public interface IAuthorizationService
    {
        Task<bool> IsUserAuthorized(User user, PermissionAccessPolicy permissionAccessPolicy, CancellationToken cancellationToken);
        IEnumerable<IPermission> GetUserPermissions(User user);
    }
}