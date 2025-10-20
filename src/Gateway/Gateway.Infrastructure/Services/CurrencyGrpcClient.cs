using Currency.Grpc;
using Gateway.Application.Interfaces;
using Gateway.Application.Models.Currency;
using Gateway.Infrastructure.Mappers.Currency;
using Grpc.Core;

namespace Gateway.Infrastructure.Services
{
    /// <summary>
    /// GRPC клиент проксриуемый к сервису валют
    /// </summary>
    public class CurrencyGrpcClient : ICurrencyGrpcClient
    {
        private readonly CurrencyProtoService.CurrencyProtoServiceClient _client;

        public CurrencyGrpcClient(CurrencyProtoService.CurrencyProtoServiceClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Получить список всех актуальных валют в базе
        /// </summary>
        /// <param name="jwtToken">JWT Токен для последующей авторизации на другом микросервисе</param>
        /// <returns></returns>
        public async Task<List<CurrencyModel>> GetAllCurrenciesAsync(string jwtToken)
        {
            // Добавляем JWT из HttpContext в GRPC метаданные
            var headers = new Metadata();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                // Если токен приходит с "Bearer ", оставляем как есть
                headers.Add("Authorization", jwtToken);
            }
            var response = await _client.GetAllCurrenciesAsync(new Empty(), headers);
            return response.Currencies.Select(c => c.ToModel()).ToList();
        }

        /// <summary>
        /// Получить валюту по ID
        /// </summary>
        /// <param name="id">GUID валюты</param>
        /// <param name="jwtToken">JWT Токен для последующей авторизации на другом микросервисе</param>
        /// <returns></returns>
        public async Task<CurrencyModel?> GetCurrencyByIdAsync(string id, string jwtToken)
        {
            // Добавляем JWT из HttpContext в GRPC метаданные
            var headers = new Metadata();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                // Если токен приходит с "Bearer ", оставляем как есть
                headers.Add("Authorization", jwtToken);
            }
            var request = new GetCurrencyByIdRequest { Id = id };
            var response = await _client.GetCurrencyByIdAsync(request, headers);
            return response?.ToModel();
        }

        /// <summary>
        /// Получить избранные валюты пользователя
        /// </summary>
        /// <param name="userId">ID Пользователя</param>
        /// <param name="jwtToken">JWT Токен для последующей авторизации на другом микросервисе</param>
        /// <returns></returns>
        public async Task<List<CurrencyModel>> GetUserCurrenciesAsync(string userId, string jwtToken)
        {
            var headers = new Metadata();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                // Если токен приходит с "Bearer ", оставляем как есть
                headers.Add("Authorization", jwtToken);
            }
            var request = new GetUserCurrenciesRequest { UserId = userId };
            var response = await _client.GetUserCurrenciesAsync(request, headers);
            var currencies = response.Currencies.ToList();
            return currencies.Select(c => c.ToModel()).ToList();
        }

        /// <summary>
        /// Добавить избранную валюту пользователю
        /// </summary>
        /// <param name="userId">GUID пользователя</param>
        /// <param name="currencyId">GUID валюты</param>
        /// <param name="jwtToken">JWT Токен для последующей авторизации на другом микросервисе</param>
        /// <returns></returns>
        public async Task<bool> AddFavoriteCurrencyAsync(string userId, string currencyId, string jwtToken)
        {
            var headers = new Metadata();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                // Если токен приходит с "Bearer ", оставляем как есть
                headers.Add("Authorization", jwtToken);
            }
            var response = await _client.AddFavoriteCurrencyAsync(new ModifyFavoriteCurrencyRequest
            {
                UserId = userId,
                CurrencyId = currencyId
            }, headers);

            return response.Success;
        }

        /// <summary>
        /// Удалить валюту у пользователя
        /// </summary>
        /// <param name="userId">GUID пользователя</param>
        /// <param name="currencyId">GUID валюты</param>
        /// <param name="jwtToken">JWT Токен для последующей авторизации на другом микросервисе</param>
        /// <returns></returns>
        public async Task<bool> RemoveFavoriteCurrencyAsync(string userId, string currencyId, string jwtToken)
        {
            var headers = new Metadata();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                // Если токен приходит с "Bearer ", оставляем как есть
                headers.Add("Authorization", jwtToken);
            }
            var response = await _client.RemoveFavoriteCurrencyAsync(new ModifyFavoriteCurrencyRequest
            {
                UserId = userId,
                CurrencyId = currencyId
            }, headers);

            return response.Success;
        }
    }

}
