using AuthRPolicy.Core.Roles;
using System.Collections.Generic;

namespace AuthRPolicy.Core
{
    public interface IUser // TODO: just User?
    {
        string UserName { get; } // TODO: consider UserName type
        IEnumerable<Role> Roles { get; }
    }
}