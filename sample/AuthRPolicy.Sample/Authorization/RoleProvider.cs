using AuthRPolicy.Core.Roles;
using AuthRPolicy.Sample.Authorization.Permissions;

namespace AuthRPolicy.Sample.Authorization
{
    public class RoleProvider : DefaultRoleProvider
    {
        public RoleProvider()
        {
            AddRole(Roles.DocumentCreator, CreateDocument.Default);
            AddRole(Roles.DocumentReviewer, ListDocuments.AsReviwer);
            AddRole(Roles.Admin, ListDocuments.AsAdmin);

            ConnectPermissions(CreateDocument.Default, ListDocuments.AsOwner);
        }
    }
}
