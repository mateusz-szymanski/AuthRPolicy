using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Mediator
{
    public class MediatorRunner
    {
        private readonly ServiceProvider _serviceProvider;

        public MediatorRunner(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            await using var serviceScope = _serviceProvider.CreateAsyncScope();
            var mediator = serviceScope.ServiceProvider.GetRequiredService<IMediator>();

            return await mediator.Send(request);
        }
    }
}
