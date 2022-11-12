using AuthRPolicy.MediatRExtensions.Services;
using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using AuthRPolicy.Sample.IoC;
using AuthRPolicy.Sample.Tests.Extensions;
using AuthRPolicy.Sample.Tests.UserMocking;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace AuthRPolicy.Sample.Tests.Initialization
{
    public class ApplicationBuilder
    {
        public async Task<IApplication> Build()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .Build();

            services
                .AddNullLogger()
                .AddApplicationServices(configuration)
                .AddMediatR(typeof(ApplicationBuilder).Assembly);

            services
                .AddSingleton<UserSwitcher, UserSwitcher>()
                .ReplaceService<ICurrentUserService>(sp => sp.GetRequiredService<UserSwitcher>(), ServiceLifetime.Singleton);

            services.AddSingleton<IStorageManager, StorageManager>();

            ConfigureDatabase(services);

            var serviceProvider = services.BuildServiceProvider();

            var application = new Application(serviceProvider);
            await application.Initialize();

            return application;
        }

        private static void ConfigureDatabase(IServiceCollection services)
        {
            var databaseId = Guid.NewGuid();
            var databaseName = $"sample-tests-{databaseId}";
            var dbConnectionStringBuilder = new DbConnectionStringBuilder();
            dbConnectionStringBuilder["Data Source"] = @"(LocalDb)\AuthR.Sample";
            dbConnectionStringBuilder["Initial Catalog"] = databaseName;
            dbConnectionStringBuilder["Integrated Security"] = "SSPI";
            //dbConnectionStringBuilder["AttachDBFilename"] = @$"|DataDirectory|\Sample-{databaseId}.mdf";

            var connectionString = dbConnectionStringBuilder.ConnectionString;

            var dbContextOptions = new DbContextOptionsBuilder<SampleDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            services.ReplaceService<DbContextOptions>(dbContextOptions);
            services.ReplaceService(dbContextOptions);
        }
    }
}
