using Currency.Application.Interfaces;
using Currency.Application.Services;
using Microsoft.Extensions.DependencyInjection;



namespace Currency.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Регистрация всех сервисов бизнес-логики
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IFavoriteService, FavoriteService>();

            // Бизнес-логика фонового сервиса обновления валют
            services.AddScoped<ICurrencyUpdaterService, CurrencyUpdaterService>();


            return services;
        }
    }
}
