using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    /// <summary>
    /// Вспомогательный класс сборки конфигурации из оносительного пути для миграций
    /// Расчитываем на то, что структура проекта будет соблюдатся по схеме Solution\src\Project\Project.Layer
    /// </summary>
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot BuildConfiguration(string relativeProjectPath)
        {
            //Вычисляем путь до текущего Project, отрезаем два уровня и переходим в папку с целевым репозиторием
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), relativeProjectPath);
            return new ConfigurationBuilder().SetBasePath(basePath)
                                             .AddJsonFile("appsettings.json", optional: false)
                                             .AddEnvironmentVariables()
                                             .Build();
        }
    }
}
