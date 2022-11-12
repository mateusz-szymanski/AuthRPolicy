using AuthRPolicy.Sample.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public class StorageConfigurationProvider
    {
        private readonly Guid _databaseId;

        public StorageConfigurationProvider()
        {
            _databaseId = Guid.NewGuid();
        }

        public DbContextOptions<SampleDbContext> GetDbOptions()
        {
            var databaseName = $"sample-tests-{_databaseId}";
            var dbConnectionStringBuilder = new DbConnectionStringBuilder();
            dbConnectionStringBuilder["Data Source"] = @"(LocalDb)\AuthR.Sample";
            dbConnectionStringBuilder["Initial Catalog"] = databaseName;
            dbConnectionStringBuilder["Integrated Security"] = "SSPI";
            //dbConnectionStringBuilder["AttachDBFilename"] = @$"|DataDirectory|\Sample-{_databaseId}.mdf";

            var connectionString = dbConnectionStringBuilder.ConnectionString;

            var dbContextOptions = new DbContextOptionsBuilder<SampleDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return dbContextOptions;
        }
    }
}
