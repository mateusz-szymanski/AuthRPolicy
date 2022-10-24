using AuthRPolicy.Core.Roles.Exceptions;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AuthRPolicy.Core.Roles
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

        [ExcludeFromCodeCoverage]
        private string GetDebuggerDisplay()
        {
            return Name;
        }
    }
}