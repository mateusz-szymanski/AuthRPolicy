using AuthRAccessPolicy.Core.Permissions;
using AuthRAccessPolicy.Core.Roles;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AuthRAccessPolicy.Core.IoC
{
    public class AuthorizationOptions
    {
        public IEnumerable<Assembly> Assemblies { get; set; } = Enumerable.Empty<Assembly>();
        public IRoleProviderBuilder RolesBuilder { get; } = new DefaultRoleProviderBuilder();
    }
}
