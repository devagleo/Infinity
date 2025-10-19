using Currency.Application.Interfaces;
using Currency.Application.Models;
using Currency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Application.Services
{
    /// <summary>
    /// Сервис бизнес-логики обработку избранных пользователями валют
    /// </summary>
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoritelRepository _repository;
        private readonly ICurrencyRepository _currencyRepository;

        public FavoriteService(IFavoritelRepository favRepository, ICurrencyRepository currencyRepository)
        {
            _repository = favRepository;
            _currencyRepository = currencyRepository;
        }

        /// <summary>
        /// Получить список избранных валют пользователем
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CurrencyModel>> GetUserFavoritesAsync(Guid userId)
        {
            var favoriteCurrenciesIds = await _repository.GetFavoritesAsync(userId);
            var currencies = await _currencyRepository.GetCurrenciesByIdsAsync(favoriteCurrenciesIds.ToList());
            var result = currencies.Select(x => new CurrencyModel
                {
                    Name = x.Name,
                    ValuteId = x.ValuteId,
                    Rate = x.Rate,
                }).ToList();
            return result;
        }
        
        /// <summary>
        /// Добавить избранную валюту пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public async Task AddFavoriteAsync(Guid userId, Guid currencyId)
        {
            await _repository.AddFavoriteAsync(userId, currencyId);
        }

        /// <summary>
        /// Удалить избранную валюту у пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public async Task RemoveFavoriteAsync(Guid userId, Guid currencyId)
        {
            await _repository.RemoveFavoriteAsync(userId, currencyId);
        }
    }
}
