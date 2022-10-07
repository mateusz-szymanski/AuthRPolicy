using Authorization.AccessPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Tests.Stubs
{
    internal class AccessPolicy3Stub : IAccessPolicy
    {
        public string Name => "policy-3";
    }
}
