using Currency.Grpc;
using Gateway.Application.Interfaces;
using Gateway.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Settings;
using User.Grpc;



namespace Gateway.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Читаем секцию Jwt
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddScoped<IUserGrpcClient, UserGrpcClient>();
            services.AddScoped<ICurrencyGrpcClient, CurrencyGrpcClient>();

            //// Grpc Клиенты
            services.AddGrpcClient<UserProtoService.UserProtoServiceClient>(o =>
            {
                o.Address = new Uri(configuration["UrlGrpcMicroservice:User"]);
            });

            services.AddGrpcClient<CurrencyProtoService.CurrencyProtoServiceClient>(o =>
            {
                o.Address = new Uri(configuration["UrlGrpcMicroservice:Currency"]);
            });

            return services;
        }
    }
}
