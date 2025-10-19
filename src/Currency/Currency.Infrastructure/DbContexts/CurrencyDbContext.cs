using Currency.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Currency.Infrastructure.DbContexts
{
    /// <summary>
    /// Основной контекст для работы с базой валют
    /// </summary>
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Таблица валют
        /// </summary>
        public DbSet<CurrencyEntity> Currencies { get; set; }

        /// <summary>
        /// Таблица избранных валют по пользователям
        /// </summary>
        public DbSet<FavoriteEntity> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
