using Authorization.Roles;
using System.Collections.Generic;

namespace Authorization.Tests.Sample
{
    public static class Roles
    {
        public static Role DocumentReader = new("document-reader");
        public static Role DocumentCreator = new("document-creator");

        public static IEnumerable<Role> AllRoles => new[]
        {
            DocumentReader, DocumentCreator
        };
    }
}
