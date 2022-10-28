using AuthRPolicy.MediatRExtensions.Services;
using AuthRPolicy.Sample.Domain;
using AuthRPolicy.Sample.Domain.DocumentAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Features.CreateDocument
{
    public class CreateDocumentCommandHandler : AsyncRequestHandler<CreateDocumentCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDocumentCommandHandler(
            ICurrentUserService currentUserService,
            IDocumentRepository documentRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _documentRepository = documentRepository;
            _unitOfWork = unitOfWork;
        }

        protected override async Task Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserService.GetCurrentUser(cancellationToken);
            var owner = currentUser.UserName;

            var document = new Document(request.Title, owner, request.Reviewer);

            await _documentRepository.Add(document);

            await _unitOfWork.SaveChanges();
        }
    }
}
