using User.Application;
using User.Grpc.Interceptors;
using User.Grpc.Services;
using User.Infrastructure;

namespace User.Grpc
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
            app.MapGrpcService<UserGrpcService>();
            app.MapGet("/", () => "GRPC ��������");

            app.Run();
        }
    }
}