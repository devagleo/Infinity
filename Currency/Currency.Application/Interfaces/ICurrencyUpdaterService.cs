namespace Currency.Application.Interfaces
{
    /// <summary>
    /// Сервис бизнес-логики фонового обновления валют из ЦБ
    /// </summary>
    public interface ICurrencyUpdaterService
    {
        /// <summary>
        /// Обновить валюты, получаем из ЦБ, сравниваем с репозиторием и обновляем при необходимости
        /// </summary>
        /// <returns></returns>
        Task UpdateCurrenciesAsync();

    }
}
