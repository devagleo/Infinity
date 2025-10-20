using Gateway.Application.Interfaces;
using Gateway.Application.Models;
using Gateway.Currency.Grpc;
using Gateway.Grpc.Mappers;
using Grpc.Core;

namespace Gateway.Grpc.Services
{
    /// <summary>
    /// Реализация GRPC
    /// </summary>
    public class GatewayCurrencyGrpcService : GatewayCurrencyProtoService.GatewayCurrencyProtoServiceBase
    {
        private readonly ICurrencyGrpcClient _currencyClient;

        public GatewayCurrencyGrpcService(ICurrencyGrpcClient currencyClient)
        {
            _currencyClient = currencyClient;
        }

        /// <summary>
        /// Получить все актуальные валюты из базы
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetAllCurrenciesResponse> GetAllCurrencies(Empty request, ServerCallContext context)
        {
            var list = await _currencyClient.GetAllCurrenciesAsync(context.RequestHeaders.GetValue("Authorization")!);

            return new GetAllCurrenciesResponse
            {
                Currencies = { list.Select(x => x.ToCurrencyGrpc()) }
            };
        }

        /// <summary>
        /// Получить валюту по GUID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<CurrencyGrpcDTO> GetCurrencyById(GetCurrencyByIdRequest request, ServerCallContext context)
        {
            var dto = await _currencyClient.GetCurrencyByIdAsync(request.Id, context.RequestHeaders.GetValue("Authorization")!);
            return dto?.ToCurrencyGrpc() ?? new CurrencyGrpcDTO();
        }

        /// <summary>
        /// Получить список избранных валют пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetUserCurrenciesResponse> GetUserCurrencies(GetUserCurrenciesRequest request, ServerCallContext context)
        {
            var currencies = await _currencyClient.GetUserCurrenciesAsync(request.UserId, context.RequestHeaders.GetValue("Authorization")!);

            var response = new GetUserCurrenciesResponse();
            response.Currencies.AddRange(currencies.Select(c => new CurrencyGrpcDTO
            {
                Id = c.Id,
                Name = c.Name,
                Rate = (double)c.Rate
            }));

            return response;
        }

        /// <summary>
        /// Добавить валюту в избранное пользователю
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ModifyFavoriteCurrencyResponse> AddFavoriteCurrency(ModifyFavoriteCurrencyRequest request, ServerCallContext context)
        {
            try
            {
                var success = await _currencyClient.AddFavoriteCurrencyAsync(request.UserId, request.CurrencyId, context.RequestHeaders.GetValue("Authorization")!);
                return new ModifyFavoriteCurrencyResponse
                {
                    Success = success,
                    Message = success ? "Added" : "Ошибка при добавлении"
                };
            }
            catch (Exception ex)
            {
                return new ModifyFavoriteCurrencyResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Удалить валюту из избранных у пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ModifyFavoriteCurrencyResponse> RemoveFavoriteCurrency(ModifyFavoriteCurrencyRequest request, ServerCallContext context)
        {
            try
            {
                var success = await _currencyClient.RemoveFavoriteCurrencyAsync(request.UserId, request.CurrencyId, context.RequestHeaders.GetValue("Authorization")!);
                return new ModifyFavoriteCurrencyResponse
                {
                    Success = success,
                    Message = success ? "Removed" : "Ошибка удаления"
                };
            }
            catch (Exception ex)
            {
                return new ModifyFavoriteCurrencyResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
