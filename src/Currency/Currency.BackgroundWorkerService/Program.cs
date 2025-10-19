using Currency.Application;
using Currency.Infrastructure;
using Currency.WorkerService;

namespace Currency.BackgroundWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            //���������� ����������� ����
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            //������������� � ���������� XML ����� �� windows-1251
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var host = builder.Build();
            host.Run();
        }
    }
}