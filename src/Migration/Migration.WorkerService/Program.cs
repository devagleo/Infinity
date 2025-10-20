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

            // ���������� ��������� �� � ��������� ������������ EF
            builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(builder.Configuration["ConnectionStrings:UserDb"])
                       .EnableSensitiveDataLogging()   // ���������� �������� �������� ����������
                       .EnableDetailedErrors()         // ���������� ��������� ������
                       .LogTo(Console.WriteLine, LogLevel.Debug) // ����� ���� �������� EF � �������
            );

            builder.Services.AddDbContext<CurrencyDbContext>(options =>
                options.UseNpgsql(builder.Configuration["ConnectionStrings:CurrencyDb"])
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors()
                       .LogTo(Console.WriteLine, LogLevel.Debug)
            );
            // �����������
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);

            var host = builder.Build();
            host.Run();
        }
    }
}