using Currency.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Currency.WorkerService.Services
{
    /// <summary>
    /// Сервис для связи с ЦБ и скачивания валют в XML
    /// </summary>
    public class CurrencyHttpService : ICurrencyHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string Url;

        public CurrencyHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            Url = configuration["Url:CBCurrencies"] ?? throw new ArgumentNullException("Url:CBCurrencies - не указан урл скачивания курсов ЦБ");
        }

        /// <summary>
        /// Получить список валют в массиве байт для обработки стрима
        /// </summary>
        /// <returns>Массив байт, используйте MemoryStream для чтения содержимого</returns>
        public async Task<byte[]?> GetCurrenciesAsync()
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(Url);
                if (response == null)
                {
                    return null;
                }

                var bytes = await response.Content.ReadAsByteArrayAsync();
                return bytes;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Не удалось получить список валют с сайта ЦБ {Environment.NewLine}{ex.ToString}");
            }
        }
    }
}
