using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Domain.DocumentAggregate
{
    // TODO: async api
    public interface IDocumentRepository
    {
        Task Add(Document document, CancellationToken cancellationToken);
        Document GetDocument(DocumentId documentId);
    }
}
