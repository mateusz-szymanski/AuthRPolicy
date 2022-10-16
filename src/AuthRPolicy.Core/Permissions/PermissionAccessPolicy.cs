using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Permissions.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AuthRPolicy.Core.Permissions
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class PermissionAccessPolicy
    {
        public PermissionAccessPolicy(string permissionMainName, params IAccessPolicy[] accessPolices)
        {
            if (!accessPolices.Any())
                throw MissingAccessPoliciesForPermissionAccessPolicyException.New(permissionMainName);

            PermissionMainName = permissionMainName;
            AccessPolicies = accessPolices;
        }

        public string PermissionMainName { get; }
        public IEnumerable<IAccessPolicy> AccessPolicies { get; }

        private string GetDebuggerDisplay()
        {
            return $"{PermissionMainName}({AccessPolicies.Count()})";
        }
    }
}