using Authorization.Permissions;

namespace Authorization.Tests.Sample
{
    public static class DocumentPermissions
    {
        public static Permission ListDocuments = new("list-documents");
        public static Permission CreateDocument = new("create-documents");
    }
}