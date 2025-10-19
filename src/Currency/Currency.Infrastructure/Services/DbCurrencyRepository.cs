using Currency.Application.Interfaces;
using Currency.Domain.Entities;
using Currency.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Infrastructure.Services
{
    /// <summary>
    /// Сервис по работе с бд в части обработки валют
    /// </summary>
    public class DbCurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyDbContext _context;

        public DbCurrencyRepository(CurrencyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить актуальный список всех валют в базе
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CurrencyEntity>> GetAllAsync() 
        { 
            //TODO: Вынести логику в Application уровень или переименовать метод
            return await _context.Currencies.Where(x => x.EffectiveDate == DateTime.Today).ToListAsync(); 
        }

        /// <summary>
        /// Получить конкретную валюту по ID в базе
        /// </summary>
        /// <param name="id">Guid в базе</param>
        /// <returns></returns>
        public async Task<CurrencyEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        /// <summary>
        /// Получить валюту по Valute ID ЦБ из баз
        /// </summary>
        /// <param name="valuteId"></param>
        /// <returns></returns>
        public async Task<CurrencyEntity?> GetByValuteIdAsync(string valuteId)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.ValuteId == valuteId);
        }

        /// <summary>
        /// Добавить новую валюту в базу
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task AddAsync(CurrencyEntity currency) 
        { 
            await _context.Currencies.AddAsync(currency); await _context.SaveChangesAsync(); 
        }

        /// <summary>
        /// Обновить валюту
        /// Используется для обновления курса
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CurrencyEntity currency) 
        { 
            _context.Currencies.Update(currency); await _context.SaveChangesAsync(); 
        }

        /// <summary>
        /// Получить список валют массивом
        /// </summary>
        /// <param name="ids">List guid валют</param>
        /// <returns></returns>
        public async Task<List<CurrencyEntity>> GetCurrenciesByIdsAsync(List<Guid> ids)
        {
            var entities = await _context.Currencies.Where(x => ids.Contains(x.Id)).ToListAsync();
            return entities;
        }
    }
}
