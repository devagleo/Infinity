using Currency.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.DbContexts;
using Migration.WorkerService;

namespace Migration.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<MigrationWorker>();

            // Подключаем контексты БД с детальным логированием EF
            builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(builder.Configuration["ConnectionStrings:UserDb"])
                       .EnableSensitiveDataLogging()   // показывать реальные значения параметров
                       .EnableDetailedErrors()         // показывать детальные ошибки
                       .LogTo(Console.WriteLine, LogLevel.Debug) // вывод всех действий EF в консоль
            );

            builder.Services.AddDbContext<CurrencyDbContext>(options =>
                options.UseNpgsql(builder.Configuration["ConnectionStrings:CurrencyDb"])
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors()
                       .LogTo(Console.WriteLine, LogLevel.Debug)
            );
            // Логирование
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);

            var host = builder.Build();
            host.Run();
        }
    }
}