using AuthRPolicy.Core.AccessPolicy;

namespace AuthRPolicy.Core.TestAssemblies.ValidCheckers.Policy1
{
    public record AccessPolicy1 : IAccessPolicy
    {
        public string Name => "AccessPolicy1";
    }
}