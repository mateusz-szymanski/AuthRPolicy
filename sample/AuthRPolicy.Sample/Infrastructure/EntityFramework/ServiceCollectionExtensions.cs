using AuthRPolicy.Sample.Domain;
using AuthRPolicy.Sample.Domain.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthRPolicy.Sample.Infrastructure.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services)
        {
            services.AddDbContextFactory<SampleDbContext>(options =>
            {
                options.UseSqlServer("name=ConnectionStrings:Sample");
            });

            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();

            return services;
        }
    }
}
