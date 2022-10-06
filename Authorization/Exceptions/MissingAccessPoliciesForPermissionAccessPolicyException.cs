using Authorization.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Exceptions
{
    public class MissingAccessPoliciesForPermissionAccessPolicyException : AuthorizationException
    {
        public MissingAccessPoliciesForPermissionAccessPolicyException(Permission permission)
            : this($"Permission access policy for permission {permission.Name} is missing access policies")
        {
        }

        protected MissingAccessPoliciesForPermissionAccessPolicyException(string? message) : base(message)
        {
        }

        protected MissingAccessPoliciesForPermissionAccessPolicyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MissingAccessPoliciesForPermissionAccessPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
