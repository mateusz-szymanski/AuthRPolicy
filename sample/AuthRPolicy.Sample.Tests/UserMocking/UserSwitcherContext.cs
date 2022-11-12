using AuthRPolicy.Core;
using System;

namespace AuthRPolicy.Sample.Tests.UserMocking
{
    public class UserSwitcherContext : IDisposable
    {
        private readonly UserSwitcher _userSwitcher;
        private readonly User _rememberedUser;

        public UserSwitcherContext(UserSwitcher userSwitcher, User rememberedUser)
        {
            _userSwitcher = userSwitcher;
            _rememberedUser = rememberedUser;
        }

        public void Dispose()
        {
            _userSwitcher.ChangeUser(_rememberedUser);
        }
    }
}
