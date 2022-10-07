﻿using System;
using System.Runtime.Serialization;

namespace Authorization.Exceptions
{
    public class MissingRoleNameException : AuthorizationException
    {
        public MissingRoleNameException()
            : this($"Role must have a name")
        {
        }

        protected MissingRoleNameException(string? message) : base(message)
        {
        }

        protected MissingRoleNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MissingRoleNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}