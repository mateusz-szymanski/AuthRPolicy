namespace AuthRPolicy.Core.AccessPolicy
{
    // TODO: Repo name AuthRIt, AuthRPolicy, AuthRPolicy?
    public record EmptyAccessPolicy : IAccessPolicy
    {
        public string Name => "Empty";
    }
}