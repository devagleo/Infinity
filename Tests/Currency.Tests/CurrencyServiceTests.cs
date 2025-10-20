using Currency.Application.Interfaces;
using Currency.Application.Models;
using Currency.Application.Services;
using Currency.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Currency.Application.Tests
{
    public class CurrencyServiceTests
    {
        private readonly Mock<ICurrencyRepository> _currencyRepoMock;
        private readonly CurrencyService _currencyService;

        public CurrencyServiceTests()
        {
            _currencyRepoMock = new Mock<ICurrencyRepository>();
            _currencyService = new CurrencyService(_currencyRepoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Currencies()
        {
            // Arrange
            var currencies = new List<CurrencyEntity>
            {
                new CurrencyEntity { Id = Guid.NewGuid(), Name = "USD", Rate = 1.1m, ValuteId = "R01235" },
                new CurrencyEntity { Id = Guid.NewGuid(), Name = "EUR", Rate = 0.9m, ValuteId = "R01239" }
            };
            _currencyRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(currencies);

            // Act
            var result = await _currencyService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Correct_Currency()
        {
            // Arrange
            var id = Guid.NewGuid();
            var currency = new CurrencyEntity { Id = id, Name = "USD", Rate = 1.1m, ValuteId = "R01235" };
            _currencyRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(currency);

            // Act
            var result = await _currencyService.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("USD", result!.Name);
        }
    }
}
