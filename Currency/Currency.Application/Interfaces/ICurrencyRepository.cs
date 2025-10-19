using Currency.Domain.Entities;

namespace Currency.Application.Interfaces
{
    /// <summary>
    /// Сервис по работе с БД в части обработки валют
    /// </summary>
    public interface ICurrencyRepository
    {
        /// <summary>
        /// Получить все актуальные валюты из БД
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CurrencyEntity>> GetAllAsync();

        /// <summary>
        /// Получить валюту по ID базы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CurrencyEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить валюту по Valute ID ЦБ из базы
        /// </summary>
        /// <param name="valuteId"></param>
        /// <returns></returns>
        Task<CurrencyEntity?> GetByValuteIdAsync(string valuteId);


        /// <summary>
        /// Добавить валюту в базу
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task AddAsync(CurrencyEntity currency);

        /// <summary>
        /// Обновить валюту в базу
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task UpdateAsync(CurrencyEntity currency);

        /// <summary>
        /// Получить массив валют по массиву ID
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<CurrencyEntity>> GetCurrenciesByIdsAsync(List<Guid> ids);

    }
}
