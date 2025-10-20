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
            _logger.LogInformation("����� ������� �������� ��");
            try
            {
                // ������� �������, ��� ��� ��������� �������������� ��� Scope, � Worker � HosteService ��� Singleton
                using var scope = _scopeFactory.CreateScope();
                // �������� �����
                var _currencyDbContext = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();
                // �������� �������������
                var _userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

                _logger.LogInformation("�������� �������� UserDb...");
                await _userDbContext.Database.MigrateAsync(stoppingToken);
                _logger.LogInformation("UserDb �������� ���������.");

                _logger.LogInformation("�������� ������� CurrencyDb...");
                await _currencyDbContext.Database.MigrateAsync(stoppingToken);
                _logger.LogInformation("CurrencyDb �������� ���������.");

                _logger.LogInformation("��� �������� ���������");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��������!");
            }
        }
    }
}
