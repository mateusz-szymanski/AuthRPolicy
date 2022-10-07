using Authorization.Roles;
using System.Collections.Generic;

namespace Authorization.Tests.Stubs
{
    internal class RoleProviderStub : IRoleProvider
    {
        private readonly List<Role> _roles;

        public RoleProviderStub()
        {
            _roles = new();
        }

        public RoleProviderStub WithRoles(params Role[] roles)
        {
            _roles.AddRange(roles);
            return this;
        }

        public IEnumerable<Role> GetAvailableRoles()
        {
            return _roles;
        }
    }
}
