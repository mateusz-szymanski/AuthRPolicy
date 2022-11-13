using AuthRPolicy.Sample.Domain.DocumentAggregate;
using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Infrastructure
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly SampleDbContext _dbContext;

        public DocumentRepository(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Document document, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(document, cancellationToken);
        }

        public Task<Document> GetDocument(DocumentId documentId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
