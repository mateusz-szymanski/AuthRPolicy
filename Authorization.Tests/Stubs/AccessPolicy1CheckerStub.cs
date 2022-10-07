using Authorization.AccessPolicy;
using System;

namespace Authorization.Tests.Stubs
{
    internal class AccessPolicy1CheckerStub : IAccessPolicyChecker<AccessPolicy1Stub>
    {
        private Func<IUser, AccessPolicy1Stub, bool> _hasAccessFunc;

        public AccessPolicy1CheckerStub()
        {
            _hasAccessFunc = (_, _) => true;
        }

        public AccessPolicy1CheckerStub ShouldReturn(Func<IUser, AccessPolicy1Stub, bool> hasAccessFunc)
        {
            _hasAccessFunc = hasAccessFunc;
            return this;
        }

        public bool HasAccess(IUser user, AccessPolicy1Stub accessPolicy)
        {
            return _hasAccessFunc(user, accessPolicy);
        }
    }
}
