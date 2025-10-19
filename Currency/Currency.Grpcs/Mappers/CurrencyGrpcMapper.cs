using Currency.Application.Models;

namespace Currency.Grpc.Mappers
{
    /// <summary>
    /// Вспомогательный сервис-маппер перевода моделей из слоя GRPC в Application и обратно
    /// </summary>
    public static class CurrencyGrpcMapper
    {
        /// <summary>
        /// Из CurrencyModel в CurrencyGrpcDTO
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CurrencyGrpcDTO ToGrpc(this CurrencyModel model)
        {
            return new CurrencyGrpcDTO
            {
                Id = model.Id.ToString(),
                Name = model.Name,
                Rate = (double)model.Rate // gRPC double
            };
        }

        /// <summary>
        /// Из CurrencyGrpcDTO в CurrencyModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CurrencyModel ToModel(this CurrencyGrpcDTO dto)
        {
            return new CurrencyModel
            {
                Id = Guid.Parse(dto.Id),
                Name = dto.Name,
                Rate = (decimal)dto.Rate, // обратно в decimal
                EffectiveDate = DateTime.UtcNow
            };
        }
    }
}
