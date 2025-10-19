using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared.Helpers;

namespace User.Infrastructure.DbContexts
{
    /// <summary>
    /// Класс для формирования DB контекста в момент формирования миграции, чтобы не указывать в командной строке строку подключения
    /// Использует всегда актуальную строку
    /// </summary>
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            //Собираем конфигурация из относительного пути
            var configuration = ConfigurationHelper.BuildConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "../../User/User.Grpc"));
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            optionsBuilder.UseNpgsql(configuration["ConnectionStrings:UserDb"]);

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
