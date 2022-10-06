namespace Authorization.AccessPolicy
{
    public record EmptyAccessPolicy : IAccessPolicy
    {
        public string Name => "Empty";
    }
}