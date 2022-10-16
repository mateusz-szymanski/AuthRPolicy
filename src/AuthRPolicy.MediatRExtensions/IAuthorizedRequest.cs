using AuthRPolicy.Core.Permissions;

namespace AuthRPolicy.Core.MediatRExtensions
{
    public interface IAuthorizedRequest
    {
        // TODO: Ignore when request is logged
        public PermissionAccessPolicy PermissionAccessPolicy { get; }
    }
}
