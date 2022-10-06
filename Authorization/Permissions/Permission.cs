using Authorization.Exceptions;

namespace Authorization.Permissions
{
    public record Permission
    {
        public string Name { get; }

        public Permission(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new MissingPermissionNameException();

            Name = name;
        }
    }
}