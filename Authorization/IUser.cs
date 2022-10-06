using Authorization.Roles;
using System.Collections.Generic;

namespace Authorization
{
    public interface IUser
    {
        string UserName { get; }
        IEnumerable<Role> Roles { get; }
    }
}