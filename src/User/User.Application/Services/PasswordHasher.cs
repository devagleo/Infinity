using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using User.Application.Interfaces;

namespace User.Application.Services
{
    /// <summary>
    /// Вспомогательный сервис работы с паролями и хэшами
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        public PasswordHasher()
        {
            
        }

        /// <summary>
        /// Создать хэш пароля из plain text
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            ));
            return $"{Convert.ToBase64String(salt)}.{hash}";
        }

        /// <summary>
        /// Проверить, подходит ли указанный хэш к паролю
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            ));

            return hash == parts[1];
        }
    }
}
