using Authorization.AccessPolicy;

namespace Authorization.MediatRExtensions
{
    public interface IAuthorizedRequest
    {
        public PermissionAccessPolicy PermissionAccessPolicy { get; }
    }
}
