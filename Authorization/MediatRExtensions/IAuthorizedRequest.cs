using Authorization.Permissions;

namespace Authorization.MediatRExtensions
{
    public interface IAuthorizedRequest
    {
        public PermissionAccessPolicy PermissionAccessPolicy { get; }
    }
}
