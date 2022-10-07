using Authorization.AccessPolicy;
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
        public static MissingAccessPoliciesForPermissionAccessPolicyException New(string permissionMainName)
        {
            var message = $"Permission access policy for permission {permissionMainName} is missing access policies";
            return new MissingAccessPoliciesForPermissionAccessPolicyException(message);
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
