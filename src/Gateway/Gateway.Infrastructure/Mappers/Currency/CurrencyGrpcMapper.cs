using Currency.Grpc;
using Gateway.Application.Models.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Infrastructure.Mappers.Currency
{
    /// <summary>
    /// Вспомогательный класс перевода маппинга моделей между слоями
    /// </summary>
    public static class CurrencyGrpcMapper
    {
        public static CurrencyModel ToModel(this CurrencyGrpcDTO dto)
        {
            if (dto == null) return new CurrencyModel();

            return new CurrencyModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Rate = (decimal)dto.Rate
            };
        }
    }
}
