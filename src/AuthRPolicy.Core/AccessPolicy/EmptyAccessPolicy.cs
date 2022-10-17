namespace AuthRPolicy.Core.AccessPolicy
{
    // TODO: Repo name AuthRIt, AuthRPolicy, AuthRPolicy?
    // TODO: Rename to DefaultAccessPolicy? No, not really?

    /// <summary>
    /// Simple access policy that comes with EmptyAccessPolicyChecker that always return true.
    /// </summary>
    public record EmptyAccessPolicy : IAccessPolicy
    {
        public string Name => "Empty";
    }
}