using AuthRPolicy.Core.AccessPolicy;

namespace AuthRPolicy.Core.TestAssemblies.ValidCheckers.Policy2
{
    public record AccessPolicy2 : IAccessPolicy
    {
        public string Name => "AccessPolicy2";
    }
}