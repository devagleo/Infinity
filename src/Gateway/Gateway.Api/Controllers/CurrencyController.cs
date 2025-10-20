using Gateway.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyGrpcClient _currencyClient;

        public CurrencyController(ICurrencyGrpcClient currencyClient)
        {
            _currencyClient = currencyClient;
        }

        // GET api/currency
        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var list = await _currencyClient.GetAllCurrenciesAsync(HttpContext.Request.Headers["Authorization"].FirstOrDefault()!);
            return Ok(list);
        }

        // GET api/currency/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyById(string id)
        {
            var currency = await _currencyClient.GetCurrencyByIdAsync(id, HttpContext.Request.Headers["Authorization"].FirstOrDefault()!);
            if (currency == null) return NotFound();
            return Ok(currency);
        }

        // GET api/currency/{userId}/currencies
        [HttpGet("{userId}/currencies")]
        public async Task<IActionResult> GetUserCurrencies(string userId)
        {
            var currencies = await _currencyClient.GetUserCurrenciesAsync(userId, HttpContext.Request.Headers["Authorization"].FirstOrDefault()!);
            return Ok(currencies);
        }

        // POST api/currency/{userId}/favorites/{currencyId}
        [HttpPost("{userId}/favorites/{currencyId}")]
        public async Task<IActionResult> AddFavoriteCurrency(string userId, string currencyId)
        {
            var success = await _currencyClient.AddFavoriteCurrencyAsync(userId, currencyId, HttpContext.Request.Headers["Authorization"].FirstOrDefault()!);
            if (success) return Ok(new { Message = "Добавлено" });
            return BadRequest(new { Message = "Ошибка при добавлении" });
        }

        // DELETE api/currency/{userId}/favorites/{currencyId}
        [HttpDelete("{userId}/favorites/{currencyId}")]
        public async Task<IActionResult> RemoveFavoriteCurrency(string userId, string currencyId)
        {
            var success = await _currencyClient.RemoveFavoriteCurrencyAsync(userId, currencyId, HttpContext.Request.Headers["Authorization"].FirstOrDefault()!);
            if (success) return Ok(new { Message = "Удалено" });
            return BadRequest(new { Message = "Ошибка удаления" });
        }
    }
}
