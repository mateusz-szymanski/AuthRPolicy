using System;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.Exceptions
{
    [Serializable]
    public class MissingUserNameException : AuthorizationException
    {
        public static MissingUserNameException New()
        {
            return new("User must have a user name");
        }

        protected MissingUserNameException(string? message) : base(message)
        {
        }

        protected MissingUserNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
