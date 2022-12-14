using AuthRPolicy.Core.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AuthRPolicy.Core.IoC.Exceptions
{
    [Serializable]
    public class IncorrectAccessPolicyCheckerDefinitionException : AuthorizationException
    {
        public static IncorrectAccessPolicyCheckerDefinitionException New(Type accessPolicyCheckerInterfaceType)
        {
            var message = $"There must exactly one implementation for {accessPolicyCheckerInterfaceType.FullName}";
            return new IncorrectAccessPolicyCheckerDefinitionException(message);
        }

        protected IncorrectAccessPolicyCheckerDefinitionException(string? message) : base(message)
        {
        }

        [ExcludeFromCodeCoverage]
        protected IncorrectAccessPolicyCheckerDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
