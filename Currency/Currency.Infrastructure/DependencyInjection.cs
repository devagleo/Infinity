using Currency.Application.Interfaces;
using Currency.Infrastructure.DbContexts;
using Currency.Infrastructure.Services;
using Currency.WorkerService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Settings;

namespace Currency.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Подключение PostgreSQL
            services.AddDbContext<CurrencyDbContext>(options =>
                options.UseNpgsql(configuration["ConnectionStrings:CurrencyDb"]));
            // Конфигурируем EF работать в шарповом формате дат
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


            // Репозитории
            services.AddScoped<ICurrencyRepository, DbCurrencyRepository>();
            services.AddScoped<IFavoritelRepository, DbFavoriteRepository>();

            // Параметры для JWT Интерцептора
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            // Внешние источники
            services.AddScoped<ICurrencyHttpService, CurrencyHttpService>();
            services.AddHttpClient();

            return services;
        }
    }
}
