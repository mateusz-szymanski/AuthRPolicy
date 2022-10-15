using Authorization.Permissions;
using Authorization.Tests.Sample.AccessPolicies.DocumentPermissions;

namespace Authorization.Tests.Sample.IoC
{
    public class RoleProvider : AbstractRoleProvider
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
