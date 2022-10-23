namespace AuthRPolicy.Sample.Commands
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Reviewer { get; set; } = string.Empty;
    };
}
