using AuthRAccessPolicy.Core.AccessPolicy;

namespace AuthRAccessPolicy.Core.Tests.Stubs
{
    internal class AccessPolicy1Stub : IAccessPolicy
    {
        public string Name => "policy-1";
    }
}
