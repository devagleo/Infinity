using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Settings
{
    /// <summary>
    /// Класс хранения JWT параметров, для лёгкого подключения в DI
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Секрет, которым сервер авторизации подписывает JWT
        /// </summary>
        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// Сервис, выдавший JWT
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Адрес сервера выдавшего JWT
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Срок действия токена
        /// Используется только на сервисе выдачи токенов
        /// </summary>
        public int ExpiryMinutes { get; set; } = 120;
    }
}
