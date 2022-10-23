using AuthRPolicy.MediatRExtensions.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuthRPolicy.MediatRExtensions.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationBehavior(this IServiceCollection services)
        {
            // TODO: tests
            services.AddScoped<ICurrentUserService, HttpContextBasedCurrentUserService>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            return services;
        }
    }
}
