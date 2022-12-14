using AuthRPolicy.Core.Permissions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AuthRPolicy.Core.Tests.Assertions
{
    internal static class AssertPermission
    {
        public static void EqualIgnoringOrder(IEnumerable<IPermission> expected, IEnumerable<IPermission> actual)
        {
            var sortedExpected = expected.OrderBy(ep => ep.FullName);
            var sortedActual = actual.OrderBy(ep => ep.FullName);

            Assert.Equal(sortedExpected, sortedActual);
        }
    }
}
