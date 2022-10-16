using AuthRAccessPolicy.Core.Permissions;

namespace AuthRAccessPolicy.Core.MediatRExtensions
{
    public interface IAuthorizedRequest
    {
        // TODO: Ignore when request is logged
        public PermissionAccessPolicy PermissionAccessPolicy { get; }
    }
}
