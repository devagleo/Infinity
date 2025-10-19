using Microsoft.EntityFrameworkCore;
using User.Domain;

namespace User.Infrastructure.DbContexts
{

    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        { 

        }

        /// <summary>
        /// Пользователи в базе
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }
    }
}
