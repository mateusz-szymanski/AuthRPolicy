using AuthRPolicy.Core.Permissions;
using System;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Exceptions
{
    public class PermissionAlreadyConnectedException : AuthorizationException
    {
        public static PermissionAlreadyConnectedException New(IPermission permission)
        {
            var message = $"Permission {permission.FullName} already added";
            return new PermissionAlreadyConnectedException(message);
        }

        protected PermissionAlreadyConnectedException(string? message) : base(message)
        {
        }

        protected PermissionAlreadyConnectedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PermissionAlreadyConnectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
