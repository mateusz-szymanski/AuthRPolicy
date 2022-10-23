using AuthRPolicy.Core;
using AuthRPolicy.Core.AccessPolicy;
using System;

namespace AuthRPolicy.Core.Tests.Stubs
{
    internal class AccessPolicy2CheckerStub : IAccessPolicyChecker<AccessPolicy2Stub>
    {
        private Func<User, AccessPolicy2Stub, bool> _hasAccessFunc;

        public AccessPolicy2CheckerStub()
        {
            _hasAccessFunc = (_, _) => true;
        }

        public AccessPolicy2CheckerStub ShouldReturn(Func<User, AccessPolicy2Stub, bool> hasAccessFunc)
        {
            _hasAccessFunc = hasAccessFunc;
            return this;
        }

        public bool HasAccess(User user, AccessPolicy2Stub accessPolicy)
        {
            return _hasAccessFunc(user, accessPolicy);
        }
    }
}
