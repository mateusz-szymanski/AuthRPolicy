using AuthRPolicy.Core.Exceptions;
using AuthRPolicy.Core.Roles;
using System.Collections.Generic;
using System.Linq;

namespace AuthRPolicy.Core
{
    public class User
    {
        public string UserName { get; } // TODO: consider UserName type
        public IEnumerable<Role> Roles { get; }

        public User(string userName, IEnumerable<Role> roles)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw MissingUserNameException.New();

            UserName = userName;
            Roles = roles.Distinct();
        }
    }
}