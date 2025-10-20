using Gateway.Grpc.Interceptors;
using Gateway.Grpc.Services;
using Gateway.Infrastructure;

namespace Gateway.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ��������� JWT ����������� � ���������� ��� � GRPC.
            builder.Services.AddScoped<JwtAuthInterceptor>();
            builder.Services.AddGrpc(options =>
            {
                options.Interceptors.Add<JwtAuthInterceptor>();
            });

            // ����������� ����
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GatewayCurrencyGrpcService>();
            app.MapGrpcService<GatewayUserGrpcService>();
            app.MapGet("/", () => "GRPC ��������");

            app.Run();
        }
    }
}