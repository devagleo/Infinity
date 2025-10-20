using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Application.DTOs;
using User.Application.Interfaces;
using User.Application.Services;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _passwordHasherMock = new Mock<IPasswordHasher>();

        _userService = new UserService(
            _userRepositoryMock.Object,
            _jwtTokenServiceMock.Object,
            _passwordHasherMock.Object
        );
    }

    [Fact]
    public async Task RegisterAsync_Should_Add_User_When_Username_Not_Exists()
    {
        // Arrange
        var request = new RegisterRequestDTO { Username = "testuser", Password = "password" };
        _userRepositoryMock.Setup(r => r.GetByLoginAsync(request.Username)).ReturnsAsync((User.Domain.UserEntity?)null);
        _passwordHasherMock.Setup(h => h.HashPassword(request.Password)).Returns("hashed-password");

        // Act
        var result = await _userService.RegisterAsync(request);

        // Assert
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User.Domain.UserEntity>()), Times.Once);
        Assert.Equal("testuser", result.Name);
    }

    [Fact]
    public async Task RegisterAsync_Should_Throw_When_Username_Exists()
    {
        // Arrange
        var request = new RegisterRequestDTO { Username = "existinguser", Password = "password" };
        _userRepositoryMock.Setup(r => r.GetByLoginAsync(request.Username))
                           .ReturnsAsync(new User.Domain.UserEntity { Name = request.Username });

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.RegisterAsync(request));
    }

    [Fact]
    public async Task LoginAsync_Should_Return_Token_When_Credentials_Correct()
    {
        // Arrange
        var request = new LoginRequestDTO { Username = "testuser", Password = "password" };
        var user = new User.Domain.UserEntity { Name = "testuser", PasswordHash = "hashed-password" };

        _userRepositoryMock.Setup(r => r.GetByLoginAsync(request.Username)).ReturnsAsync(user);
        _passwordHasherMock.Setup(h => h.VerifyPassword(request.Password, user.PasswordHash)).Returns(true);
        _jwtTokenServiceMock.Setup(s => s.GenerateToken(user)).Returns("fake-jwt-token");

        // Act
        var result = await _userService.LoginAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("fake-jwt-token", result.Token);
    }

    [Fact]
    public async Task LoginAsync_Should_Return_Null_When_Credentials_Incorrect()
    {
        // Arrange
        var request = new LoginRequestDTO { Username = "testuser", Password = "wrongpassword" };
        var user = new User.Domain.UserEntity { Name = "testuser", PasswordHash = "hashed-password" };

        _userRepositoryMock.Setup(r => r.GetByLoginAsync(request.Username)).ReturnsAsync(user);
        _passwordHasherMock.Setup(h => h.VerifyPassword(request.Password, user.PasswordHash)).Returns(false);

        // Act
        var result = await _userService.LoginAsync(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_UserDTO_When_User_Exists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var user = new User.Domain.UserEntity { Id = id, Name = "testuser" };
        _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("testuser", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_User_Not_Found()
    {
        // Arrange
        var id = Guid.NewGuid();
        _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((User.Domain.UserEntity?)null);

        // Act
        var result = await _userService.GetByIdAsync(id);

        // Assert
        Assert.Null(result);
    }
}
