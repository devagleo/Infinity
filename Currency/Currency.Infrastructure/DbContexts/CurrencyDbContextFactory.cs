using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared.Helpers;

namespace Currency.Infrastructure.DbContexts
{
    /// <summary>
    /// Класс для формирования DB контекста в момент формирования миграции, чтобы не указывать в командной строке строку подключения
    /// Использует всегда актуальную строку подключения к БД
    /// </summary>
    public class CurrencyDbContextFactory : IDesignTimeDbContextFactory<CurrencyDbContext>
    {
        public CurrencyDbContext CreateDbContext(string[] args)
        {
            var configuration = ConfigurationHelper.BuildConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "../../Currency/Currency.Grpc"));
            var optionsBuilder = new DbContextOptionsBuilder<CurrencyDbContext>();
            optionsBuilder.UseNpgsql(configuration["ConnectionStrings:CurrencyDb"]);

            return new CurrencyDbContext(optionsBuilder.Options);
        }
    }
}
