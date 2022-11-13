using AuthRPolicy.Core.AccessPolicy;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public Task<bool> HasAccess(User user, AccessPolicy2Stub accessPolicy, CancellationToken cancellationToken)
        {
            var hasAccess = _hasAccessFunc(user, accessPolicy);
            return Task.FromResult(hasAccess);
        }
    }
}
