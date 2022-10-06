using System.Collections.Generic;

namespace Authorization.Roles
{
    public interface IRoleProvider
    {
        IEnumerable<Role> GetAvailableRoles();
    }
}