using AuthRAccessPolicy.Core;
using AuthRAccessPolicy.Core.AccessPolicy;
using System;

namespace AuthRAccessPolicy.Core.Tests.Stubs
{
    internal class AccessPolicy2CheckerStub : IAccessPolicyChecker<AccessPolicy2Stub>
    {
        private Func<IUser, AccessPolicy2Stub, bool> _hasAccessFunc;

        public AccessPolicy2CheckerStub()
        {
            _hasAccessFunc = (_, _) => true;
        }

        public AccessPolicy2CheckerStub ShouldReturn(Func<IUser, AccessPolicy2Stub, bool> hasAccessFunc)
        {
            _hasAccessFunc = hasAccessFunc;
            return this;
        }

        public bool HasAccess(IUser user, AccessPolicy2Stub accessPolicy)
        {
            return _hasAccessFunc(user, accessPolicy);
        }
    }
}
