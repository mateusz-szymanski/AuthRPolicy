using AuthRPolicy.Core.Roles;

namespace AuthRPolicy.Sample.Authorization
{
    public static class Roles
    {
        public readonly static Role DocumentReviewer = new("document-reviewer");
        public readonly static Role DocumentCreator = new("document-creator");
        public readonly static Role Admin = new("admin");
    }
}
