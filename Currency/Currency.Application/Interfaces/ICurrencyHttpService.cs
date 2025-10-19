namespace Currency.Application.Interfaces
{
    /// <summary>
    /// Сервис взаимодействия с ЦБ по http
    /// </summary>
    public interface ICurrencyHttpService
    {
        /// <summary>
        /// Получить список валют для обработки
        /// </summary>
        /// <returns></returns>
        Task<byte[]?> GetCurrenciesAsync();

    }
}
