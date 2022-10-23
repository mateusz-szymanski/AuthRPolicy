using AuthRPolicy.Core.Permissions;

namespace AuthRPolicy.Core.Roles
{
    public interface IDefaultRoleProviderBuilder
    {
        /// <summary>
        /// Connects role with permissions.
        /// </summary>
        /// <param name="role">Role.</param>
        /// <param name="permissions">Permissions assigned to the role.</param>
        /// <exception cref="RoleAlreadyAddedException">Given role is added for the second time.</exception>
        /// <returns>Builder.</returns>
        IDefaultRoleProviderBuilder AddRole(Role role, params IPermission[] permissions);

        /// <summary>
        /// Connects permissions. Every role that contains given permission will also have additionalPermissions.
        /// </summary>
        /// <param name="permission">Permission.</param>
        /// <param name="additionalPermissions">Additional permissions for given permission.</param>
        /// <exception cref="PermissionAlreadyConnectedException">Given permission is added for the second time.</exception>
        /// <returns>Builder.</returns>
        IDefaultRoleProviderBuilder ConnectPermissions(IPermission permission, params IPermission[] additionalPermissions);

        internal IRoleProvider Build();
    }
}