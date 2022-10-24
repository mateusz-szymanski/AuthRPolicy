using AuthRPolicy.Core.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Roles.Exceptions
{
    [Serializable]
    public class MissingRoleNameException : AuthorizationException
    {
        public MissingRoleNameException()
            : this($"Role must have a name")
        {
        }

        protected MissingRoleNameException(string? message) : base(message)
        {
        }

        [ExcludeFromCodeCoverage]
        protected MissingRoleNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
