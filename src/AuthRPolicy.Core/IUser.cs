using AuthRPolicy.Core.Roles;
using System.Collections.Generic;

namespace AuthRPolicy.Core
{
    public interface IUser
    {
        string UserName { get; }
        IEnumerable<Role> Roles { get; }
    }
}