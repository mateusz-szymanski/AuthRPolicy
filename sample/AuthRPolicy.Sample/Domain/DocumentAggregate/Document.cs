namespace AuthRPolicy.Sample.Domain.DocumentAggregate
{
    public class Document
    {
        public DocumentId DocumentId { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public string Reviewer { get; set; }

        public Document(string title, string owner, string reviewer)
        {
            DocumentId = DocumentId.New();
            Title = title;
            Owner = owner;
            Reviewer = reviewer;
        }
    }
}
