using Microsoft.Extensions.DependencyInjection;
using User.Application.Interfaces;
using User.Application.Services;

namespace User.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Регистрация всех сервисов бизнес-логики
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
