using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Roles;
using AuthRPolicy.Core.Services.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Core.Services
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly ILogger<AuthorizationService> _logger;
        private readonly IRoleProvider _roleProvider;
        private readonly IServiceProvider _serviceProvider;

        public AuthorizationService(
            ILogger<AuthorizationService> logger,
            IRoleProvider roleProvider,
            IServiceProvider serviceProvider)
        {
            _roleProvider = roleProvider;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IPermission> GetUserPermissions(User user)
        {
            var allRoles = _roleProvider.GetAvailableRoles();
            var userRoles = allRoles.Intersect(user.Roles);
            var userPermissions = userRoles.SelectMany(ur => _roleProvider.GetPermissionsForRole(ur)).Distinct();

            _logger.LogDebug("User {userName} permissions: {permissions}", user.UserName, userPermissions.Select(up => up.FullName));

            return userPermissions;
        }

        public async Task<bool> IsUserAuthorized(User user, PermissionAccessPolicy permissionAccessPolicy, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Authorizing user {userName} for {permission}", user.UserName, permissionAccessPolicy.PermissionMainName);

            var userPermissions = GetUserPermissions(user);

            var matchingUserPermissions = userPermissions.Where(up => up.HasMainNameEqualTo(permissionAccessPolicy.PermissionMainName));
            var hasRequiredPermission = matchingUserPermissions.Any();
            if (!hasRequiredPermission)
            {
                _logger.LogInformation("User {userName} has no required permission {permission}", user.UserName, permissionAccessPolicy.PermissionMainName);
                return false;
            }

            _logger.LogDebug("User {userName} has required permission {permission}", user.UserName, permissionAccessPolicy.PermissionMainName);

            var matchingAccessPolicies = from accessPolicy in permissionAccessPolicy.AccessPolicies
                                         join userPermission in matchingUserPermissions on accessPolicy.GetType() equals userPermission.AccessPolicyType
                                         select accessPolicy;

            _logger.LogDebug("About to check access policies: {accessPolicies}", matchingAccessPolicies.Select(ap => ap.Name));

            foreach (var accessPolicy in matchingAccessPolicies)
            {
                var hasAccess = await CheckAccessPolicy(user, accessPolicy, cancellationToken);

                if (hasAccess)
                {
                    _logger.LogInformation("Access granted for {userName} based on {accessPolicy} access policy", user.UserName, accessPolicy.Name);
                    return true;
                }
            }

            _logger.LogInformation("No access policy can authorize user {userName}", user.UserName);

            return false;
        }

        private async Task<bool> CheckAccessPolicy(User user, IAccessPolicy accessPolicy, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Checking {accessPolicy} access policy...", accessPolicy.Name);

            var accessPolicyType = accessPolicy.GetType();
            var accessPolicyCheckerType = typeof(IAccessPolicyChecker<>).MakeGenericType(accessPolicyType);

            var accessPolicyChecker = _serviceProvider.GetService(accessPolicyCheckerType);

            if (accessPolicyChecker is null)
                throw MissingAccessPolicyCheckerException.New(accessPolicy);

            var hasAccessMethodInfo = accessPolicyCheckerType.GetMethod(nameof(IAccessPolicyChecker<IAccessPolicy>.HasAccess));
            var hasAccessTaskObject = hasAccessMethodInfo!.Invoke(accessPolicyChecker, new object[] { user, accessPolicy, cancellationToken });
            var hasAccessTask = hasAccessTaskObject as Task<bool>;

            var hasAccess = await hasAccessTask!;

            _logger.LogDebug("Access policy {accessPolicy} authorization result: {hasAccess}", accessPolicy.Name, hasAccess);

            return hasAccess;
        }
    }
}