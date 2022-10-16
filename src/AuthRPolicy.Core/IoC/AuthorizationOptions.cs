using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Roles;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AuthRPolicy.Core.IoC
{
    public class AuthorizationOptions
    {
        public IEnumerable<Assembly> Assemblies { get; set; } = Enumerable.Empty<Assembly>();
        public IRoleProviderBuilder RolesBuilder { get; } = new DefaultRoleProvider();
    }
}
