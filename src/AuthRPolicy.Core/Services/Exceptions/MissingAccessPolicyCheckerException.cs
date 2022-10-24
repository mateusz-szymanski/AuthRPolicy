using AuthRPolicy.Core.AccessPolicy;
using AuthRPolicy.Core.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Services.Exceptions
{
    [Serializable]
    public class MissingAccessPolicyCheckerException : AuthorizationException
    {
        public static MissingAccessPolicyCheckerException New(IAccessPolicy accessPolicy)
        {
            var message = $"No checker defined for '{accessPolicy.Name}' access policy";
            return new MissingAccessPolicyCheckerException(message);
        }

        protected MissingAccessPolicyCheckerException(string? message) : base(message)
        {
        }

        [ExcludeFromCodeCoverage]
        protected MissingAccessPolicyCheckerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
