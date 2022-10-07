using Authorization.Roles;
using System.Collections.Generic;

namespace Authorization.Tests.Sample
{
    public static class Roles
    {
        public static Role DocumentReviewer = new("document-reviewer");
        public static Role DocumentCreator = new("document-creator");
        public static Role Admin = new("admin");

        public static IEnumerable<Role> AllRoles => new[]
        {
            DocumentReviewer, DocumentCreator, Admin
        };
    }
}
