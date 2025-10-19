using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Settings;
using User.Application.Interfaces;
using User.Infrastructure.DbContexts;
using User.Infrastructure.Services;


namespace User.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Подключение PostgreSQL
            services.AddDbContext<UserDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:UserDb"]));

            // Репозитории
            services.AddScoped<IUserRepository, DbUserRepository>();

            // Подгрузка доп конфигурации для JWT
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            // Вспомогательные сервисы
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
