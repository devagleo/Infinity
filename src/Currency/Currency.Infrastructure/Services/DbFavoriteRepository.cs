using Currency.Application.Interfaces;
using Currency.Domain.Entities;
using Currency.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Currency.Infrastructure.Services
{
    /// <summary>
    /// Сервис по работе с БД в части обработки избранных пользователями валют
    /// </summary>
    public class DbFavoriteRepository : IFavoritelRepository
    {
        private readonly CurrencyDbContext _context;

        public DbFavoriteRepository(CurrencyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Полуить список избранных пользвателем валют
        /// </summary>
        /// <param name="userId">Guid Пользвателя</param>
        /// <returns></returns>
        public async Task<IEnumerable<Guid>> GetFavoritesAsync(Guid userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.CurrencyId)
                .ToListAsync();
        }

        /// <summary>
        /// Добавить валюту в избранное пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public async Task AddFavoriteAsync(Guid userId, Guid currencyId)
        {
            var exists = await _context.Favorites.AnyAsync(f => f.UserId == userId && f.CurrencyId == currencyId);
            // уже добавлено
            if (exists) return; 

            _context.Favorites.Add(new FavoriteEntity { UserId = userId, CurrencyId = currencyId });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить валюту из избранного у пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public async Task RemoveFavoriteAsync(Guid userId, Guid currencyId)
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.CurrencyId == currencyId);
            if (favorite == null) return;

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
        }
    }
}
