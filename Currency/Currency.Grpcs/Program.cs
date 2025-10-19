using Currency.Infrastructure;
using Currency.Application;
using Currency.Grpc.Interceptors;
using Currency.Grpc.Services;

namespace Currency.Grpcs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            //Подключаем зависимости слоёв
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            //Подключаем интерцептор валидации JWT в GRPC
            builder.Services.AddScoped<JwtAuthInterceptor>();

            //Подключаем GRPC с JWT
            builder.Services.AddGrpc(options =>
            {
                options.Interceptors.Add<JwtAuthInterceptor>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<CurrencyGrpcService>();
            app.MapGet("/", () => "GRPC доступен");

            app.Run();
        }
    }
}