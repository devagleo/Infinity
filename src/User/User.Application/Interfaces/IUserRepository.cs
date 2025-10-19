using User.Domain;

namespace User.Application.Interfaces
{
    /// <summary>
    /// Сервис работы с БД
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Добавить пользователя в базу
        /// </summary>
        /// <param name="user">Сущность пользователя уже с хешированным паролем</param>
        /// <returns></returns>
        Task AddAsync(UserEntity user);

        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="username">Логин пользователя</param>
        /// <returns></returns>
        Task<UserEntity?> GetByLoginAsync(string username);

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        /// <param name="Id">GUID пользователя</param>
        /// <returns></returns>
        Task<UserEntity?> GetByIdAsync(Guid Id);
    }
}
