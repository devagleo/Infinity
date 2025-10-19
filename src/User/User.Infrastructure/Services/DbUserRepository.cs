using Microsoft.EntityFrameworkCore;
using User.Application.Interfaces;
using User.Domain;
using User.Infrastructure.DbContexts;

namespace User.Infrastructure.Services
{
    /// <summary>
    /// Сервис работы с базой данных пользователей
    /// </summary>
    public class DbUserRepository : IUserRepository
    {
        private readonly UserDbContext _db;

        public DbUserRepository(UserDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Добавить пользователя в базу
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddAsync(UserEntity user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Получить пользователя по его логину
        /// </summary>
        /// <param name="username">Логин пользователя</param>
        /// <returns></returns>
        public async Task<UserEntity?> GetByLoginAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Name == username);
        }

        /// <summary>
        /// Получить пользователя по его ID
        /// </summary>
        /// <param name="Id">GUID пользователя</param>
        /// <returns></returns>
        public async Task<UserEntity?> GetByIdAsync(Guid Id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }
    }
}
