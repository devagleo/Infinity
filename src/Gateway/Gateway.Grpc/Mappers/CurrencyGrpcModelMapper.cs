using Gateway.Currency.Grpc;
using Gateway.Application.Models.Currency;

namespace Gateway.Grpc.Mappers
{
    /// <summary>
    /// Вспомогательный класс маппинг моделей между GRPC и Infrastructure
    /// </summary>
    public static class CurrencyGrpcModelMapper
    {
        public static CurrencyGrpcDTO ToCurrencyGrpc(this CurrencyModel model)
        {
            return new CurrencyGrpcDTO
            {
                Id = model.Id,
                Name = model.Name,
                Rate = (double)model.Rate
            };
        }
    }
}
