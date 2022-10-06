using System;
using System.Runtime.Serialization;

namespace Authorization.Exceptions
{
    public class MissingPermissionNameException : AuthorizationException
    {
        public MissingPermissionNameException()
            : this($"Permission must have a name")
        {
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
