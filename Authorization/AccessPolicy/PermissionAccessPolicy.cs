using Authorization.Exceptions;
using Authorization.Permissions;
using System.Collections.Generic;
using System.Linq;

namespace Authorization.AccessPolicy
{
    public class PermissionAccessPolicy
    {
        public PermissionAccessPolicy(Permission permission, params IAccessPolicy[] accessPolices)
        {
            if (!accessPolices.Any())
                throw new MissingAccessPoliciesForPermissionAccessPolicyException(permission);

            Permission = permission;
            AccessPolicies = accessPolices;
        }

        public Permission Permission { get; }
        public IEnumerable<IAccessPolicy> AccessPolicies { get; }
    }
}