using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Lurch.Karma.Migrator;
using Xunit;

namespace Lurch.Karma.IntegrationTests
{
    [Collection("IntegrationTests")]
    public abstract class DatabaseIntegrationFixture : IDisposable
    {
        private static bool _database_was_reset = false;

        private const string ConnectionString =
            "Server=(LocalDb)\\MSSQLLocalDB;Database=LurchIntegrationTests;Trusted_Connection=true;";

        private readonly IDbTransaction _transaction;

        private readonly IDbConnection _connection;

        protected IDbTransaction Transaction => _transaction;

        public DatabaseIntegrationFixture()
        {
            // Rebuild database
            if (!_database_was_reset)
            {
                Program.ResetDatabase(ConnectionString);
                _database_was_reset = true;
            }

            // Create connection and transaction
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _connection.Dispose();
        }
    }
}