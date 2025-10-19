using Currency.Application.Interfaces;
using Currency.Domain.Entities;
using Currency.Domain.Models;
using Shared.Helpers;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace Currency.Application.Services
{
    /// <summary>
    /// Сервис бизнес-логики фоновой обработки валют
    /// </summary>
    public class CurrencyUpdaterService : ICurrencyUpdaterService
    {
        private readonly ICurrencyHttpService _currencyHttpService;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ILogger _logger;

        public CurrencyUpdaterService(ICurrencyHttpService currencyHttpService, ICurrencyRepository currencyRepository, ILogger<CurrencyUpdaterService> logger)
        {
            _currencyHttpService = currencyHttpService;
            _currencyRepository = currencyRepository;
            _logger = logger;
        }

        /// <summary>
        /// Обновить валюты, получаем из ЦБ, сравниваем с репозиторием и обновляем при необходимости
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCurrenciesAsync()
        {
            var bytes = await _currencyHttpService.GetCurrenciesAsync();
            if (bytes == null)
            {
                _logger.LogWarning("ЦБ не вернул валюты");
                return;
            }
            //Парсим валюты из XML
            using var reader = new MemoryStream(bytes);
            var serializer = new XmlSerializer(typeof(CBCurrencyResponseDTO));
            var data = serializer.Deserialize(reader) as CBCurrencyResponseDTO;
            if (data == null)
            {
                _logger.LogWarning("Не удалось сериализовать данные");
                return;
            }

            foreach (var currencyDTO in data.ValuteItems)
            {
                var currency = await _currencyRepository.GetByValuteIdAsync(currencyDTO.Id);
                //Если валюты нет в базе или она вчерашняя
                if (currency == null || currency.EffectiveDate != DateHelper.ParseCbrDateOrToday(data.Date))
                {
                    var entity = new CurrencyEntity();
                    entity.EffectiveDate = DateHelper.ParseCbrDateOrToday(data.Date);
                    entity.Rate = currencyDTO.VunitRate;
                    entity.ValuteId = currencyDTO.Id;
                    entity.Name = currencyDTO.Name;
                    await _currencyRepository.AddAsync(entity);
                }

                //Если курс валюты не совпадает с данными в базе
                else if (currency.Rate != currencyDTO.VunitRate)
                {
                    currency.Rate = currencyDTO.VunitRate;
                    await _currencyRepository.UpdateAsync(currency);
                }
            }
        }
    }
}
