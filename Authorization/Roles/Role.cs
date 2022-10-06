using Authorization.Exceptions;

namespace Authorization.Roles
{
    public record Role
    {
        public string Name { get; }

        public Role(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new MissingRoleNameException();

            Name = name;
        }
    }
}