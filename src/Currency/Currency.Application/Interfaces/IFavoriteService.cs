using Currency.Application.Models;

namespace Currency.Application.Interfaces
{
    /// <summary>
    /// Сервис бизнес-логики работы с избранными валютами пользователей
    /// </summary>
    public interface IFavoriteService
    {
        /// <summary>
        /// Получить список избранных валют пользователем
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<CurrencyModel>> GetUserFavoritesAsync(Guid userId);

        /// <summary>
        /// Добавить избранную валюту пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        Task AddFavoriteAsync(Guid userId, Guid currencyId);

        /// <summary>
        /// Удалить избарнную валюту у пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        Task RemoveFavoriteAsync(Guid userId, Guid currencyId);
    }
}
