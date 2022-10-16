using AuthRPolicy.Core.AccessPolicy;

namespace AuthRPolicy.Core.Tests.Stubs
{
    internal class AccessPolicy1Stub : IAccessPolicy
    {
        public string Name => "policy-1";
    }
}
