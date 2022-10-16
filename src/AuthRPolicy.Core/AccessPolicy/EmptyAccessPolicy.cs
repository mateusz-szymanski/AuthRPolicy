namespace AuthRPolicy.Core.AccessPolicy
{
    // TODO: Repo name AuthRIt, AuthRPolicy, AuthRPolicy?
    // TODO: Rename to DefaultAccessPolicy? No, not really?
    public record EmptyAccessPolicy : IAccessPolicy
    {
        public string Name => "Empty";
    }
}