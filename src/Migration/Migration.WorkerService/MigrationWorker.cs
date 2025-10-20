using Currency.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using User.Infrastructure.DbContexts;

namespace Migration.WorkerService
{
    public class MigrationWorker : BackgroundService
    {
        private readonly ILogger<MigrationWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MigrationWorker(ILogger<MigrationWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Migration service started");

            await MigrateDatabaseAsync<UserDbContext>("UserDb");
            await MigrateDatabaseAsync<CurrencyDbContext>("CurrencyDb");

            _logger.LogDebug("Migration service finished");
            Environment.Exit(0);
            
        }
        private async Task MigrateDatabaseAsync<TContext>(string databaseName) where TContext : DbContext
        {
            const int maxRetries = 10;
            const int delayMs = 2000;

            int attempt = 0;
            while (attempt < maxRetries)
            {
                attempt++;
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<TContext>();
                    var connString = context.Database.GetConnectionString();

                    // —оздание базы, если еЄ нет
                    await EnsureDatabaseExistsAsync(connString, databaseName);

                    _logger.LogDebug($"Applying migrations for {databaseName}...");
                    await context.Database.MigrateAsync();
                    _logger.LogDebug($"Migrations applied successfully for {databaseName}");
                    return; // успех, выходим из цикла
                }
                catch (Npgsql.NpgsqlException ex)
                {
                    _logger.LogWarning(ex, $"Attempt {attempt}/{maxRetries} failed to connect to {databaseName}. Retrying in {delayMs}ms...");
                    await Task.Delay(delayMs);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Migration failed for {databaseName} on attempt {attempt}/{maxRetries}");
                    throw;
                }
            }
        }

        private async Task EnsureDatabaseExistsAsync(string originalConnString, string databaseName)
        {
            // ћен€ем Database= на postgres дл€ подключени€ к существующей системе
            var builder = new NpgsqlConnectionStringBuilder(originalConnString)
            {
                Database = "postgres"
            };

            using var conn = new NpgsqlConnection(builder.ConnectionString);
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
            var exists = await cmd.ExecuteScalarAsync();

            if (exists == null)
            {
                _logger.LogDebug($"Database '{databaseName}' does not exist Ч creating...");
                cmd.CommandText = $"CREATE DATABASE \"{databaseName}\"";
                await cmd.ExecuteNonQueryAsync();
                _logger.LogDebug($"Database '{databaseName}' created");
            }
            else
            {
                _logger.LogDebug($"Database '{databaseName}' already exists");
            }
        }
    }
}
