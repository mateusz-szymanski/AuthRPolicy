using AuthRPolicy.Core;
using AuthRPolicy.MediatRExtensions.Services;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.UserMocking
{
    public class UserSwitcher : ICurrentUserService
    {
        public User CurrentUser { get; private set; }

        public UserSwitcher()
        {
            CurrentUser = new UserBuilder(1).Build();
        }

        public Task<User> GetCurrentUser(CancellationToken cancellationToken)
        {
            return Task.FromResult(CurrentUser);
        }

        public void ChangeUser(User user)
        {
            CurrentUser = user;
        }

        public UserSwitcherContext RunAsUser(User user)
        {
            var userSwitcherContext = new UserSwitcherContext(this, CurrentUser);

            ChangeUser(user);

            return userSwitcherContext;
        }
    }
}
