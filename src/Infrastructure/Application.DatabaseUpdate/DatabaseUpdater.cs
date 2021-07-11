using System;
using Application.Domain;
using DbUp;
using Microsoft.Extensions.Logging;

namespace Application.DatabaseUpdate
{
    // [DB]: The implementation of the database updater.
    public class DatabaseUpdater : IDatabaseUpdater
    {
        private readonly ILogger<DatabaseUpdater> _logger;
        private readonly object _lock = new object();
        
        public DatabaseUpdater(ILogger<DatabaseUpdater> logger)
        {
            _logger = logger;
        }

        public void UpdateDatabase(string connectionString)
        {
            lock (_lock)
            {
                _logger.Log(LogLevel.Information, "Starting database update.");

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new ArgumentException("connectionString cannot be empty.");

                EnsureDatabase.For.SqlDatabase(connectionString);

                // [DB]: Configure DbUp, here we override the default table name and tell dbup where to find our scripts.
                var dbUpdateBuilder = DeployChanges.To
                    .SqlDatabase(connectionString)
                    .JournalToSqlTable(null, "_Migrations")
                    .WithScriptsEmbeddedInAssembly(typeof(DatabaseUpdater).Assembly)
                    .WithTransaction()
                    .LogScriptOutput();

                var dbUpdateEngine = dbUpdateBuilder.Build();
                if (dbUpdateEngine.IsUpgradeRequired() == false)
                {
                    _logger.Log(LogLevel.Information, "Database update not required.");
                    return;
                }

                var operation = dbUpdateEngine.PerformUpgrade();
                if (operation.Successful == false)
                {
                    _logger.Log(LogLevel.Warning, operation.Error, "Database update failed.");
                    return;
                }

                _logger.Log(LogLevel.Information, "Database update success.");
            }
        }
    }
}