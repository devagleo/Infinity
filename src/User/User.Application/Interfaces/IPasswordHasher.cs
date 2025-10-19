namespace User.Application.Interfaces
{
    /// <summary>
    /// Вспомогательный сервис формирования хэшей паролей и валидаиця существующих хэшей
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Формирует хэш из пароля
        /// </summary>
        /// <param name="password">Пароль в plain text</param>
        /// <returns></returns>
        public string HashPassword(string password);

        /// <summary>
        /// Проверяет, сходится ли пароль с указанным хэшем
        /// </summary>
        /// <param name="password">Пароль в plain text</param>
        /// <param name="storedHash">Хэш пароля</param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string storedHash);
    }
}
