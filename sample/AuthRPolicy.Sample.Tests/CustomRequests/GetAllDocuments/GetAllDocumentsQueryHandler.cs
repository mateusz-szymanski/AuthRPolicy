using AuthRPolicy.Sample.Domain.DocumentAggregate;
using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.CustomRequests.GetAllDocuments
{
    public class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocumentsQuery, IEnumerable<Document>>
    {
        private readonly SampleDbContext _sampleDbContext;

        public GetAllDocumentsQueryHandler(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }

        public async Task<IEnumerable<Document>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
        {
            var results = await (from document in _sampleDbContext.Document
                                 select document).ToListAsync(cancellationToken);

            return results;
        }
    }
}
