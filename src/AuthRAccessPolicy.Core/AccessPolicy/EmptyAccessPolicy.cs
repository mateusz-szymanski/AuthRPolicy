namespace AuthRAccessPolicy.Core.AccessPolicy
{
    // Repo name AuthRIt, AuthRAccessPolicy?
    public record EmptyAccessPolicy : IAccessPolicy
    {
        public string Name => "Empty";
    }
}