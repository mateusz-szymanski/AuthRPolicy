using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Exceptions
{
    [Serializable]
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string? message) : base(message)
        {
        }

        [ExcludeFromCodeCoverage]
        protected AuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
