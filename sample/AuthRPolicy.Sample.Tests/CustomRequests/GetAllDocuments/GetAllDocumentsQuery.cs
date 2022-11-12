using AuthRPolicy.Sample.Domain.DocumentAggregate;
using MediatR;
using System.Collections.Generic;

namespace AuthRPolicy.Sample.Tests.CustomRequests.GetAllDocuments
{
    public class GetAllDocumentsQuery : IRequest<IEnumerable<Document>>
    {
    }
}
