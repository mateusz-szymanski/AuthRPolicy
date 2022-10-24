using AuthRPolicy.Core;
using AuthRPolicy.Core.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AuthRPolicy.MediatRExtensions.Exceptions
{
    [Serializable]
    public class UserUnauthorizedException : AuthorizationException
    {
        public static UserUnauthorizedException New(User user, IAuthorizedRequest request)
        {
            var message = $"User {user.UserName} is not authorized to execute request {request.GetType().Name}";
            return new UserUnauthorizedException(message);
        }

        protected UserUnauthorizedException(string? message) : base(message)
        {
        }

        [ExcludeFromCodeCoverage]
        protected UserUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
