using AuthRPolicy.MediatRExtensions.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuthRPolicy.MediatRExtensions.IoC
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers per scope AuthorizationBehavior and HttpContextBasedCurrentUserService.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddAuthorizationBehavior(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, HttpContextBasedCurrentUserService>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            return services;
        }

        /// <summary>
        /// Registers per scope AuthorizationBehavior and custom ICurrentUserService type.
        /// </summary>
        /// <typeparam name="TCurrentUserService">Custom implementation of ICurrentUserService.</typeparam>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddAuthorizationBehavior<TCurrentUserService>(this IServiceCollection services)
            where TCurrentUserService : class, ICurrentUserService
        {
            services.AddScoped<ICurrentUserService, TCurrentUserService>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            return services;
        }
    }
}
