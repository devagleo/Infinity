using Currency.Application.Models;
using Currency.Domain.Entities;

namespace Currency.Application.Interfaces
{
    /// <summary>
    /// Сервис бизнес-логики работы с валютами
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Получить список актуальный валют
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CurrencyModel>> GetAllAsync();

        /// <summary>
        /// Получить валюту по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CurrencyModel?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить валюту по ID ЦБ
        /// </summary>
        /// <param name="valuteId"></param>
        /// <returns></returns>
        Task<CurrencyModel?> GetByValuteIdAsync(string valuteId);


    }
}
