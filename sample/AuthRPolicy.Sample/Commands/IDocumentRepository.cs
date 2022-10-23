namespace AuthRPolicy.Sample.Commands
{
    public interface IDocumentRepository
    {
        string GetDocumentOwner(DocumentId documentId);
        string GetDocumentReviewer(DocumentId documentId);
    }
}
