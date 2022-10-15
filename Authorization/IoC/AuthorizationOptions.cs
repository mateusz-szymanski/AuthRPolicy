using Authorization.Tests.Sample;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Authorization.IoC
{
    public class AuthorizationOptions
    {
        public IEnumerable<Assembly> Assemblies { get; set; } = Enumerable.Empty<Assembly>();
        public IRoleProviderBuilder RolesBuilder { get; } = new DefaultRoleProviderBuilder();
    }
}
