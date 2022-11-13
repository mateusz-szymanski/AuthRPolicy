using AuthRPolicy.Core.AccessPolicy;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public Task<bool> HasAccess(User user, AccessPolicy3Stub accessPolicy, CancellationToken cancellationToken)
        {
            var hasAccess = _hasAccessFunc(user, accessPolicy);
            return Task.FromResult(hasAccess);
        }
    }
}
