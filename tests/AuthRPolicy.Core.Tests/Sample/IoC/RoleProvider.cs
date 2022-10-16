using AuthRPolicy.Core.Permissions;
using AuthRPolicy.Core.Tests.Sample.AccessPolicies.DocumentPermissions;

namespace AuthRPolicy.Core.Tests.Sample.IoC
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
