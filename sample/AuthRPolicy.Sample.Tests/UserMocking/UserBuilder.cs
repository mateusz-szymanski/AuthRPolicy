using AuthRPolicy.Core;
using AuthRPolicy.Core.Roles;
using System.Collections.Generic;
using System.Linq;

namespace AuthRPolicy.Sample.Tests.UserMocking
{
    public class UserBuilder
    {
        private IEnumerable<Role> _roles;
        private string _userName;

        public UserBuilder(int sequenceNumber = 1)
        {
            _roles = Enumerable.Empty<Role>();
            _userName = $"user-{sequenceNumber}";
        }

        public UserBuilder WithRoles(params Role[] roles)
        {
            _roles = roles;
            return this;
        }

        public User Build()
        {
            return new User(_userName, _roles);
        }
    }
}
