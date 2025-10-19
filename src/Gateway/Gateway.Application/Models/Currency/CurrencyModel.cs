using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Application.Models.Currency
{
    /// <summary>
    /// ДТО между слоями GRPC сервера и GRPC Клиента
    /// </summary>
    public class CurrencyModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Rate { get; set; }
    }
}
