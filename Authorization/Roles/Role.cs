using Authorization.Exceptions;
using System.Diagnostics;

namespace Authorization.Roles
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public record Role
    {
        public string Name { get; }

        public Role(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new MissingRoleNameException();

            Name = name;
        }

        private string GetDebuggerDisplay()
        {
            return Name;
        }
    }
}