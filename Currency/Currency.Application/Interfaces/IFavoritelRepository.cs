namespace Currency.Application.Interfaces
{
    /// <summary>
    /// Сервис по работе с БД вч части обработки избранных валют пользователями
    /// </summary>
    public interface IFavoritelRepository
    {
        /// <summary>
        /// Получить список избранных валют пользователем из базы
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Guid>> GetFavoritesAsync(Guid userId);

        /// <summary>
        /// Добавить избранную валюту в базу
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        Task AddFavoriteAsync(Guid userId, Guid currencyId);

        /// <summary>
        /// Удалить из базы избранную валюту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        Task RemoveFavoriteAsync(Guid userId, Guid currencyId);
    }
}
