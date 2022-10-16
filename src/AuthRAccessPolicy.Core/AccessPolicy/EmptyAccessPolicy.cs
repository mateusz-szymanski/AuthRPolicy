namespace AuthRAccessPolicy.Core.AccessPolicy
{
    // TODO: Repo name AuthRIt, AuthRAccessPolicy?
    public record EmptyAccessPolicy : IAccessPolicy
    {
        public string Name => "Empty";
    }
}