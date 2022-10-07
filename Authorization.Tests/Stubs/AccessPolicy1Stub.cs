using Authorization.AccessPolicy;

namespace Authorization.Tests.Stubs
{
    internal class AccessPolicy1Stub : IAccessPolicy
    {
        public string Name => "policy-1";
    }
}
