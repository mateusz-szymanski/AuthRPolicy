using AuthRPolicy.Core.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AuthRPolicy.MediatRExtensions.Exceptions
{
    [Serializable]
    public class MissingUserNameClaimException : AuthorizationException
    {
        public static MissingUserNameClaimException New(string claimType)
        {
            var message = $"Token is missing claim {claimType}";
            return new MissingUserNameClaimException(message);
        }

        protected MissingUserNameClaimException(string? message) : base(message)
        {
        }

        [ExcludeFromCodeCoverage]
        protected MissingUserNameClaimException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
