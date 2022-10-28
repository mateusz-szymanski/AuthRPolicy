using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Domain.DocumentAggregate
{
    // TODO: async api
    public interface IDocumentRepository
    {
        Task Add(Document document);
        Document GetDocument(DocumentId documentId);
    }
}
