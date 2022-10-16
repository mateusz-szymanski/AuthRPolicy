using System;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Exceptions
{
    public class MissingPermissionNameException : AuthorizationException
    {
        public static MissingPermissionNameException New(string permissionMainName)
        {
            var message = "Permission must have a name";
            return new MissingPermissionNameException(message);
        }

        protected MissingPermissionNameException(string? message) : base(message)
        {
        }

        protected MissingPermissionNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MissingPermissionNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
