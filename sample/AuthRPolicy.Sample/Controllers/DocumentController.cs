using AuthRPolicy.Sample.Commands;
using AuthRPolicy.Sample.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthRPolicy.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{documentId}")]
        public async Task<Document> GetDocuments(int documentId)
        {
            return await _mediator.Send(new GetDocumentQuery(documentId));
        }
    }
}