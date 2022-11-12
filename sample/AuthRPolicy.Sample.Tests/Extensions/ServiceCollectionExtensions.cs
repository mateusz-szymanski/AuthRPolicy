using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Linq;

namespace AuthRPolicy.Sample.Tests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNullLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));

            return services;
        }

        public static IServiceCollection ReplaceService<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime)
            where TImplementation : TService
        {
            var existingDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));

            if (existingDescriptor != null)
                services.Remove(existingDescriptor);

            var newDescriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), serviceLifetime);
            services.Add(newDescriptor);

            return services;
        }

        public static IServiceCollection ReplaceService<TService>(this IServiceCollection services, TService instance)
            where TService : notnull
        {
            var existingDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));

            if (existingDescriptor != null)
                services.Remove(existingDescriptor);

            var newDescriptor = new ServiceDescriptor(typeof(TService), instance);
            services.Add(newDescriptor);

            return services;
        }

        public static IServiceCollection ReplaceService<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, object> factory,
            ServiceLifetime serviceLifetime)
            where TService : notnull
        {
            var existingDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));

            if (existingDescriptor != null)
                services.Remove(existingDescriptor);

            var newDescriptor = new ServiceDescriptor(typeof(TService), factory, serviceLifetime);
            services.Add(newDescriptor);

            return services;
        }
    }
}
