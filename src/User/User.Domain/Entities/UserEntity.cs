using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.Domain
{
    [Table("users")]
    public class UserEntity
    {
        /// <summary>
        /// Id пользователя в базе в GUID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Логин пользователя, максимум 100 символов
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Пароль в хэш виде
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
