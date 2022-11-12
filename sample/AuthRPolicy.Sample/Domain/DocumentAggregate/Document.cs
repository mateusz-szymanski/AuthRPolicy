using System;

namespace AuthRPolicy.Sample.Domain.DocumentAggregate
{
    public class Document
    {
        public int Id { get; private set; }
        public DocumentId DocumentId { get; private set; }
        public string Title { get; private set; }
        public string Owner { get; private set; }
        public string Reviewer { get; private set; }
        public DateTime? ReviewedOn { get; private set; }

        public Document(string title, string owner, string reviewer)
        {
            DocumentId = DocumentId.New();
            Title = title;
            Owner = owner;
            Reviewer = reviewer;
        }

        public void Review()
        {
            ReviewedOn = DateTime.UtcNow;
        }
    }
}
