using Currency.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.DbContexts;

namespace Migration.WorkerService
{
    public class MigrationWorker : BackgroundService
    {
        private readonly ILogger<MigrationWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public MigrationWorker(ILogger<MigrationWorker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _scopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Начат процесс миграции БД");
            try
            {
                // Создаем фабрику, так как контексты регистрируются как Scope, а Worker в HosteService как Singleton
                using var scope = _scopeFactory.CreateScope();
                // Контекст валют
                var _currencyDbContext = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();
                // Контекст пользователей
                var _userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

                _logger.LogInformation("Применаю миграцию UserDb...");
                await _userDbContext.Database.MigrateAsync(stoppingToken);
                _logger.LogInformation("UserDb миграция применена.");

                _logger.LogInformation("Применяю миграцю CurrencyDb...");
                await _currencyDbContext.Database.MigrateAsync(stoppingToken);
                _logger.LogInformation("CurrencyDb миграция применена.");

                _logger.LogInformation("Все миграции применены");

                //Сообщаем Docker Compose о завершении миграции для вызова кондицкии service_completed_successfully
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка миграции!");
                Environment.Exit(1);
            }
        }
    }
}
