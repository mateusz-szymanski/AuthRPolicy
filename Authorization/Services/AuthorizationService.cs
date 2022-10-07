using Authorization.AccessPolicy;
using Authorization.Permissions;
using Authorization.Roles;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Authorization.Services
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly IRoleProvider _roleProvider;
        private readonly IPermissionProvider _permissionProvider;
        private readonly ILogger<AuthorizationService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public AuthorizationService(
            IRoleProvider roleProvider,
            IPermissionProvider permissionProvider,
            ILogger<AuthorizationService> logger,
            IServiceProvider serviceProvider)
        {
            _roleProvider = roleProvider;
            _permissionProvider = permissionProvider;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public bool IsUserAuthorized(IUser user, PermissionAccessPolicy permissionAccessPolicy)
        {
            _logger.LogInformation("Authorizing user {userName} for {permission}", user.UserName, permissionAccessPolicy.PermissionMainName);

            var allRoles = _roleProvider.GetAvailableRoles();
            var userRoles = allRoles.Intersect(user.Roles);
            var userPermissions = userRoles.SelectMany(ur => _permissionProvider.GetPermissionsForRole(ur));

            _logger.LogInformation("User {userName} permissions {permissions}", user.UserName, userPermissions.Select(up => up.FullName));

            var matchingUserPermissions = userPermissions.Where(up => up.HasMainNameEqualTo(permissionAccessPolicy.PermissionMainName));
            var hasRequiredPermission = matchingUserPermissions.Any();
            if (!hasRequiredPermission)
            {
                _logger.LogInformation("User {userName} has no required permission {permission}", user.UserName, permissionAccessPolicy.PermissionMainName);
                return false;
            }

            _logger.LogInformation("User {userName} has required permission {permission}", user.UserName, permissionAccessPolicy.PermissionMainName);

            _logger.LogInformation("About to check access policies: {accessPolicies}", permissionAccessPolicy.AccessPolicies.Select(ap => ap.Name));

            var matchingAccessPolicies = from accessPolicy in permissionAccessPolicy.AccessPolicies
                                         join userPermission in matchingUserPermissions on accessPolicy.GetType() equals userPermission.AccessPolicyType
                                         select accessPolicy;

            foreach (var accessPolicy in matchingAccessPolicies)
            {
                var hasAccess = CheckAccessPolicy(user, accessPolicy);

                if (hasAccess)
                {
                    _logger.LogInformation("Access granted for {userName} based on {accessPolicy} access policy", user.UserName, accessPolicy.Name);
                    return true;
                }
            }

            _logger.LogInformation("No access policy can authorize user {userName}", user.UserName);

            return false;
        }

        private bool CheckAccessPolicy(IUser user, IAccessPolicy accessPolicy)
        {
            _logger.LogInformation("Checking {accessPolicy} access policy...", accessPolicy.Name);

            var accessPolicyType = accessPolicy.GetType();
            var accessPolicyCheckerType = typeof(IAccessPolicyChecker<>).MakeGenericType(accessPolicyType);

            var accessPolicyChecker = _serviceProvider.GetService(accessPolicyCheckerType);

            if (accessPolicyChecker is null)
                throw new Exception($"No checker defined for '{accessPolicy.Name}' access policy"); // TODO: custom exception

            var hasAccessMethodInfo = accessPolicyCheckerType.GetMethod(nameof(IAccessPolicyChecker<IAccessPolicy>.HasAccess));
            var hasAccessResult = hasAccessMethodInfo!.Invoke(accessPolicyChecker, new object[] { user, accessPolicy });

            var hasAccess = (bool)hasAccessResult!;

            _logger.LogInformation("Access policy {accessPolicy} authorization result: {hasAccess}", accessPolicy.Name, hasAccess);

            return hasAccess;
        }
    }
}