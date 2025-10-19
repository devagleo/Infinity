namespace User.Application.Interfaces
{
    /// <summary>
    /// Сервис работы с JWT токеном
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Генерирует JWT-токен для пользователя.
        /// </summary>
        /// <param name="user">Сущность пользователя</param>
        /// <returns>JWT токен как строка</returns>
        public string GenerateToken(Domain.UserEntity user);
    }
}
