using AuthRPolicy.Core.AccessPolicy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Core.Tests.Stubs
{
    internal class AccessPolicy1CheckerStub : IAccessPolicyChecker<AccessPolicy1Stub>
    {
        private Func<User, AccessPolicy1Stub, bool> _hasAccessFunc;

        public AccessPolicy1CheckerStub()
        {
            _hasAccessFunc = (_, _) => true;
        }

        public AccessPolicy1CheckerStub ShouldReturn(Func<User, AccessPolicy1Stub, bool> hasAccessFunc)
        {
            _hasAccessFunc = hasAccessFunc;
            return this;
        }

        public Task<bool> HasAccess(User user, AccessPolicy1Stub accessPolicy, CancellationToken cancellationToken)
        {
            var hasAccess = _hasAccessFunc(user, accessPolicy);
            return Task.FromResult(hasAccess);
        }
    }
}
