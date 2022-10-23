using AuthRPolicy.Core.Roles;

namespace AuthRPolicy.Sample.Authorization
{
    public static class Roles
    {
        public static Role DocumentReviewer { get; } = new("document-reviewer");
        public static Role DocumentCreator { get; } = new("document-creator");
        public static Role Admin { get; } = new("admin");
    }
}
