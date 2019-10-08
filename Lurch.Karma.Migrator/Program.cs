using System;
using FluentMigrator.Runner;
using Lurch.Karma.Migrator.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Lurch.Karma.Migrator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = args[0];
            RunMigration(connectionString);
        }

        public static void RunMigration(string connectionString)
        {
            var serviceProvider = CreateServices(connectionString);

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        public static void ResetDatabase(string connectionString)
        {
            var serviceProvider = CreateServices(connectionString);

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                ResetDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(AddUsersTable).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
            
        }


        private static void ResetDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            
            // Rollback to empty database
            runner.RollbackToVersion(201810030000);
            
            // Execute the migrations
            runner.MigrateUp();

        }
    }
}
