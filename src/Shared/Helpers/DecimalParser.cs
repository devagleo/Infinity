using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    /// <summary>
    /// Вспомогательный класс перевода string в Decimal
    /// </summary>
    public static class DecimalParser
    {
        /// <summary>
        /// Перевод плаваюей запятой в точку (10,94 в Decimal)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ParseCurrencyValue(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            var normalized = value.Replace(',', '.');

            if (decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                return result;

            if (decimal.TryParse(normalized, NumberStyles.Any, new CultureInfo("ru-RU"), out result))
                return result;

            return 0;
        }
    }
}
