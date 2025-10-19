using Currency.Application.Interfaces;
using Currency.Application.Models;
using Currency.Domain.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Application.Services
{
    /// <summary>
    /// Основной сервис бизнес-логики обработки валют
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _repository;

        public CurrencyService(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получить все валюты
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CurrencyModel>> GetAllAsync()
        {
            var entinies = await _repository.GetAllAsync();
            return entinies.Select(x => new CurrencyModel
            {
                Id = x.Id,
                Name = x.Name,
                Rate = x.Rate,
            });
        }

        /// <summary>
        /// Получть валюту по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CurrencyModel?> GetByIdAsync(Guid id)
        {
            var entinty =  await _repository.GetByIdAsync(id);
            if (entinty == null)
                return null;

            return new CurrencyModel
            {
                Id = entinty.Id,
                Name = entinty.Name,
                Rate = entinty.Rate,
            };
        }

        /// <summary>
        /// Получить валюту по Valute ID ЦБ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CurrencyModel?> GetByValuteIdAsync(string valuteId)
        {
            var entinty = await _repository.GetByValuteIdAsync(valuteId);
            if (entinty == null)
                return null;

            return new CurrencyModel
            {
                Id = entinty.Id,
                Name = entinty.Name,
                Rate = entinty.Rate,
            };
        }
    }
}
