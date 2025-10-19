using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    /// <summary>
    /// Вспомогательный класс переводы из даты ЦБ в шапровую дату
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// Перевести дату из 10.10.2020 в DateTime
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime ParseCbrDateOrToday(string? dateString)
        {
            if (DateTime.TryParseExact(dateString,
                                        "dd.MM.yyyy",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out var parsedDate))
            {
                return parsedDate;
            }

            return DateTime.Today;
        }
    }
}
