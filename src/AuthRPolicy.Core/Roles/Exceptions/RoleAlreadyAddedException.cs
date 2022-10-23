using AuthRPolicy.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Roles.Exceptions
{
    [Serializable]
    public class RoleAlreadyAddedException : AuthorizationException
    {
        public static RoleAlreadyAddedException New(Role role)
        {
            var message = $"Role {role.Name} already added";
            return new RoleAlreadyAddedException(message);
        }

        protected RoleAlreadyAddedException(string? message) : base(message)
        {
        }

        protected RoleAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
