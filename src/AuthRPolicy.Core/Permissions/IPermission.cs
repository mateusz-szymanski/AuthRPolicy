using System;

namespace AuthRPolicy.Core.Permissions
{
    public interface IPermission
    {
        string FullName { get; }
        public string MainName { get; }
        public string SubName { get; }
        bool HasMainNameEqualTo(string mainPermissionName);
        Type AccessPolicyType { get; }
    }
}