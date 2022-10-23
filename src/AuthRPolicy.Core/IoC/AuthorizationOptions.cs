using AuthRPolicy.Core.Roles;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AuthRPolicy.Core.IoC
{
    public class AuthorizationOptions
    {
        /// <summary>
        /// Assemblies with AccessPolicyChecker implementations. Each used IAccessPolicy must have exactly one checker.
        /// </summary>
        public IEnumerable<Assembly> Assemblies { get; set; } = Enumerable.Empty<Assembly>();

        /// <summary>
        /// Builder for in-memory DefaultRoleProvider. Registered as Singleton.
        /// </summary>
        public IDefaultRoleProviderBuilder RolesBuilder { get; } = new DefaultRoleProvider();
    }
}
