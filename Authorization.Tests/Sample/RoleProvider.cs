using Authorization.Roles;
using System.Collections.Generic;

namespace Authorization.Tests.Sample
{
    internal class RoleProvider : IRoleProvider
    {
        public IEnumerable<Role> GetAvailableRoles()
        {
            return Roles.AllRoles;
        }
    }
}
