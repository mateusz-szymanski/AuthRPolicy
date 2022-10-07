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
            _logger.LogInformation("Authorizing user {userName} for {permission}", user.UserName, permissionAccessPolicy.Permission.Name);

            var allRoles = _roleProvider.GetAvailableRoles();
            var userRoles = allRoles.Intersect(user.Roles);
            var userPermissions = userRoles.SelectMany(ur => _permissionProvider.GetPermissionsForRole(ur));

            var hasRequiredPermission = userPermissions.Contains(permissionAccessPolicy.Permission);
            if (!hasRequiredPermission)
            {
                _logger.LogInformation("User {userName} has no required permission {permission}", user.UserName, permissionAccessPolicy.Permission.Name);
                return false;
            }

            _logger.LogInformation("User {userName} has required permission {permission}", user.UserName, permissionAccessPolicy.Permission.Name);

            _logger.LogInformation("About to check access policies: {accessPolicies}", permissionAccessPolicy.AccessPolicies.Select(ap => ap.Name));
            
            foreach (var accessPolicy in permissionAccessPolicy.AccessPolicies)
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