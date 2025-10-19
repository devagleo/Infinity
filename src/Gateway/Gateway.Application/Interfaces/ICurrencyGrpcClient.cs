using Gateway.Application.Models.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Application.Interfaces
{
    /// <summary>
    /// Интерфейс доступа к проксирующему GRPC-клиенту
    /// </summary>
    public interface ICurrencyGrpcClient
    {
        Task<List<CurrencyModel>> GetAllCurrenciesAsync(string jwtToken);
        Task<CurrencyModel?> GetCurrencyByIdAsync(string id, string jwtToken);
        Task<List<CurrencyModel>> GetCurrenciesByIdsAsync(IEnumerable<string> ids, string jwtToken);

        Task<List<CurrencyModel>> GetUserCurrenciesAsync(string userId, string jwtToken);
        Task<bool> AddFavoriteCurrencyAsync(string userId, string currencyId, string jwtToken);
        Task<bool> RemoveFavoriteCurrencyAsync(string userId, string currencyId, string jwtToken);
    }
}
