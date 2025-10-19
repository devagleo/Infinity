using User.Application.DTOs;

namespace User.Application.Interfaces
{
    /// <summary>
    /// Сервис бизнес логики работы с пользователем
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Зарегистрировать пользователя в системе и добавить его в базу данных
        /// </summary>
        /// <param name="request">Модель запроса из логина и пароля</param>
        /// <returns>Возвращает GUID пользователя и статус обработки</returns>
        Task<UserDTO> RegisterAsync(RegisterRequestDTO request);

        /// <summary>
        /// Проводит авторизацию по логину и паролю и выдает JWT токен
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);

        /// <summary>
        /// Сообщает клиенту, что произведён разлогон
        /// На самом деле с HMAC JWT мы не можем отозвать токен, если только не подключим Редис или Кафку
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task LogoutAsync(string token);

        /// <summary>
        /// Получить сущность пользователя по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDTO?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить сущность пользователя по его логину
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<UserDTO?> GetByLoginAsync(string username);

    }
}
