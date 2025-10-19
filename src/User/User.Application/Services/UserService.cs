using User.Application.DTOs;
using User.Application.Interfaces;

namespace User.Application.Services
{
    /// <summary>
    /// Сервис бизнес-логики работы с пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository repository, IJwtTokenService jwtTokenService, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _jwtTokenService = jwtTokenService;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Зарегистрировать пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserDTO> RegisterAsync(RegisterRequestDTO request)
        {
            if (await _repository.GetByLoginAsync(request.Username) != null)
                throw new Exception("Пользователь с таким логином уже зарегистрирован");

            var user = new Domain.UserEntity
            {
                Name = request.Username,
                PasswordHash = _passwordHasher.HashPassword(request.Password)
            };

            await _repository.AddAsync(user);

            return new UserDTO(user);
        }

        /// <summary>
        /// Авторизовать пользователя и выдать JWT Токен
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
        {
            var user = await _repository.GetByLoginAsync(request.Username);
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return null; // неудачная авторизация

            var token = _jwtTokenService.GenerateToken(user);

            return new LoginResponseDTO { Token = token };
        }

        /// <summary>
        /// Подтвердить клиенту о разлогине
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task LogoutAsync(string token)
        {
            // JWT stateless: просто удаляем токен на клиенте.
            return Task.CompletedTask;
        }

        /// <summary>
        /// Получить пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDTO?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : new UserDTO { Id = user.Id, Name = user.Name };
        }

        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<UserDTO?> GetByLoginAsync(string username)
        {
            var user = await _repository.GetByLoginAsync(username);
            return user == null ? null : new UserDTO { Id = user.Id, Name = user.Name };
        }
    }
}
