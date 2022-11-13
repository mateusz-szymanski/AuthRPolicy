using System;
using System.Data.Common;

namespace AuthRPolicy.Sample.Tests.Initialization.Storage
{
    public class ConnectionStringProvider
    {
        private readonly Guid _databaseId;

        public ConnectionStringProvider()
        {
            _databaseId = Guid.NewGuid();
        }

        public string GetConnectionString()
        {
            var databaseName = $"sample-tests-{_databaseId}";
            var dbConnectionStringBuilder = new DbConnectionStringBuilder();
            dbConnectionStringBuilder["Data Source"] = @"(LocalDb)\AuthR.Sample";
            dbConnectionStringBuilder["Initial Catalog"] = databaseName;
            dbConnectionStringBuilder["Integrated Security"] = "SSPI";
            //dbConnectionStringBuilder["AttachDBFilename"] = @$"|DataDirectory|\Sample-{_databaseId}.mdf";

            var connectionString = dbConnectionStringBuilder.ConnectionString;

            return connectionString;
        }
    }
}
