using Currency.Application.Interfaces;
using System;

namespace Currency.WorkerService
{
    /// <summary>
    /// ������� ������ ���������� ����� �� ��
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly int _backgroundDelay;

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            if (configuration["BackgroundUpdater_Delay_Seconds"] == null || !int.TryParse(configuration["BackgroundUpdater_Delay_Seconds"], out _backgroundDelay)) 
            {
                throw new ArgumentNullException("�� ������� ��� ������� �� � �������� ������� ����� ����� ������������");
            }
        }

        /// <summary>
        /// ��������� ���� ���������� ����� �� ��
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //���������� ������� ������� � ������� �������� ��������� ������
                    //��� ��� WorkerService ��� ��� Singleton, � ������� Scope
                    using var scope = _scopeFactory.CreateScope();
                    var currencyUpdater = scope.ServiceProvider.GetRequiredService<ICurrencyUpdaterService>();
                    await currencyUpdater.UpdateCurrenciesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"������ ��� ��������� ������ �� �� {Environment.NewLine}{ex.ToString()}");
                }
                _logger.LogInformation($"��������� ���������� ����� {_backgroundDelay} ������");
                await Task.Delay(TimeSpan.FromSeconds(_backgroundDelay), stoppingToken);

            }
        }
    }
}
