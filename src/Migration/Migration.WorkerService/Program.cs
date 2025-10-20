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
            
            // Подключаем основные контекст БД
            builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(builder.Configuration["ConnectionStrings:UserDb"]));

            builder.Services.AddDbContext<CurrencyDbContext>(options =>
                options.UseNpgsql(builder.Configuration["ConnectionStrings:CurrencyDb"]));

            var host = builder.Build();
            host.Run();
        }
    }
}