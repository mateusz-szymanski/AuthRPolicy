using Authorization.Permissions;

namespace Authorization.MediatRExtensions
{
    public interface IAuthorizedRequest
    {
        // TODO: Ignore when request is logged
        public PermissionAccessPolicy PermissionAccessPolicy { get; }
    }
}
