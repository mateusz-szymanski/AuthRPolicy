using AuthRPolicy.Core.Exceptions;
using AuthRPolicy.Core.Permissions;
using System;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Roles.Exceptions
{
    [Serializable]
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

        protected PermissionAlreadyConnectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
