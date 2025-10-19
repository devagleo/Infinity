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
            
            //���������� ����������� ����
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            //���������� ����������� ��������� JWT � GRPC
            builder.Services.AddScoped<JwtAuthInterceptor>();

            //���������� GRPC � JWT
            builder.Services.AddGrpc(options =>
            {
                options.Interceptors.Add<JwtAuthInterceptor>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<CurrencyGrpcService>();
            app.MapGet("/", () => "GRPC ��������");

            app.Run();
        }
    }
}