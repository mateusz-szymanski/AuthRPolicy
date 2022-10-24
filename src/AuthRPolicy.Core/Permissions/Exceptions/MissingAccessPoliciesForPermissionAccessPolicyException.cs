using AuthRPolicy.Core.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Permissions.Exceptions
{
    [Serializable]
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

        [ExcludeFromCodeCoverage]
        protected MissingAccessPoliciesForPermissionAccessPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
