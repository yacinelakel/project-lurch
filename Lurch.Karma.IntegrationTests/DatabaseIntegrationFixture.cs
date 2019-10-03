using System;
using Lurch.Karma.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lurch.Karma.IntegrationTests
{
    [Collection("IntegrationTests")]
    public abstract class DatabaseIntegrationFixture : IDisposable
    {
        private const string ConnectionString =
            "Server=(LocalDb)\\MSSQLLocalDB;Database=LurchIntegrationTests;Trusted_Connection=true;";

        public DatabaseIntegrationFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KarmaContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            KarmaContext = new KarmaContext(optionsBuilder.Options);

            KarmaContext.Database.EnsureCreated();
        }

        protected KarmaContext KarmaContext { get; private set; }

        public void Dispose()
        {
            KarmaContext.Database.EnsureDeleted();
            KarmaContext.Dispose();
        }
    }
}
