using AuthRAccessPolicy.Core.Roles;

namespace AuthRAccessPolicy.Core.Tests.Sample
{
    public static class Roles
    {
        public static Role DocumentReviewer = new("document-reviewer");
        public static Role DocumentCreator = new("document-creator");
        public static Role Admin = new("admin");
    }
}
