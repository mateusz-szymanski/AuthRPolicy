using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AuthRPolicy.MediatRExtensions.Tests.IoC
{
    // TODO: remove?
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNullLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));

            return services;
        }
    }
}
