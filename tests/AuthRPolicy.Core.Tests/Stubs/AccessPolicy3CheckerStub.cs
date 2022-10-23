using AuthRPolicy.Core.AccessPolicy;
using System;

namespace AuthRPolicy.Core.Tests.Stubs
{
    internal class AccessPolicy3CheckerStub : IAccessPolicyChecker<AccessPolicy3Stub>
    {
        private Func<User, AccessPolicy3Stub, bool> _hasAccessFunc;

        public AccessPolicy3CheckerStub()
        {
            _hasAccessFunc = (_, _) => true;
        }

        public AccessPolicy3CheckerStub ShouldReturn(Func<User, AccessPolicy3Stub, bool> hasAccessFunc)
        {
            _hasAccessFunc = hasAccessFunc;
            return this;
        }

        public bool HasAccess(User user, AccessPolicy3Stub accessPolicy)
        {
            return _hasAccessFunc(user, accessPolicy);
        }
    }
}
