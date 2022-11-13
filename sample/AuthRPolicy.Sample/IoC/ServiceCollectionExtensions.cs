using AuthRPolicy.Sample.Authorization.IoC;
using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuthRPolicy.Sample.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

            services
                .AddApplicationAuthorization()
                .AddEntityFramework();

            return services;
        }
    }
}
