using AuthRAccessPolicy.Core.Roles;
using System.Collections.Generic;

namespace AuthRAccessPolicy.Core
{
    public interface IUser
    {
        string UserName { get; }
        IEnumerable<Role> Roles { get; }
    }
}