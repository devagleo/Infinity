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
    public class FavoriteServiceTests
    {
        private readonly Mock<IFavoritelRepository> _favRepoMock;
        private readonly Mock<ICurrencyRepository> _currencyRepoMock;
        private readonly FavoriteService _favoriteService;

        public FavoriteServiceTests()
        {
            _favRepoMock = new Mock<IFavoritelRepository>();
            _currencyRepoMock = new Mock<ICurrencyRepository>();
            _favoriteService = new FavoriteService(_favRepoMock.Object, _currencyRepoMock.Object);
        }

        [Fact]
        public async Task GetUserFavoritesAsync_Should_Return_Favorite_Currencies()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var favIds = new List<Guid> { Guid.NewGuid() };
            var currencies = new List<CurrencyEntity>
            {
                new CurrencyEntity { Id = favIds[0], Name = "USD", Rate = 1.1m, ValuteId = "R01235" }
            };

            _favRepoMock.Setup(r => r.GetFavoritesAsync(userId)).ReturnsAsync(favIds);
            _currencyRepoMock.Setup(r => r.GetCurrenciesByIdsAsync(favIds.ToList())).ReturnsAsync(currencies);

            // Act
            var result = (await _favoriteService.GetUserFavoritesAsync(userId)).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("USD", result[0].Name);
        }

        [Fact]
        public async Task AddFavoriteAsync_Should_Call_Repository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var currencyId = Guid.NewGuid();

            _favRepoMock.Setup(r => r.AddFavoriteAsync(userId, currencyId)).Returns(Task.CompletedTask);

            // Act
            await _favoriteService.AddFavoriteAsync(userId, currencyId);

            // Assert
            _favRepoMock.Verify(r => r.AddFavoriteAsync(userId, currencyId), Times.Once);
        }

        [Fact]
        public async Task RemoveFavoriteAsync_Should_Call_Repository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var currencyId = Guid.NewGuid();

            _favRepoMock.Setup(r => r.RemoveFavoriteAsync(userId, currencyId)).Returns(Task.CompletedTask);

            // Act
            await _favoriteService.RemoveFavoriteAsync(userId, currencyId);

            // Assert
            _favRepoMock.Verify(r => r.RemoveFavoriteAsync(userId, currencyId), Times.Once);
        }
    }
}
