using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Domain.DocumentAggregate
{
    public interface IDocumentRepository
    {
        Task Add(Document document, CancellationToken cancellationToken);
        Task<Document> GetDocument(DocumentId documentId, CancellationToken cancellationToken);
    }
}
