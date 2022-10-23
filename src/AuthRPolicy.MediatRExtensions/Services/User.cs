using AuthRPolicy.Core;
using AuthRPolicy.Core.Roles;

namespace AuthRPolicy.MediatRExtensions.Services
{
    public class User : IUser
    {
        public string UserName { get; }
        public IEnumerable<Role> Roles { get; }

        public User(string userName, IEnumerable<Role> roles)
        {
            UserName = userName;
            Roles = roles;
        }
    }
}
