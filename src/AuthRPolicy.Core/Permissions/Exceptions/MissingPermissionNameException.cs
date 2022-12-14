using AuthRPolicy.Core.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Permissions.Exceptions
{
    [Serializable]
    public class MissingPermissionNameException : AuthorizationException
    {
        public static MissingPermissionNameException New()
        {
            var message = "Permission must have a name";
            return new MissingPermissionNameException(message);
        }

        protected MissingPermissionNameException(string? message) : base(message)
        {
        }

        [ExcludeFromCodeCoverage]
        protected MissingPermissionNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
