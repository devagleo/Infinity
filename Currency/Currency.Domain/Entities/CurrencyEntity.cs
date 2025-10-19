using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Currency.Domain.Entities
{
    /// <summary>
    /// Таблица хранения валют
    /// </summary>
    [Table("currencies")]
    public class CurrencyEntity
    {
        /// <summary>
        /// ID валюты в базе
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// ID валюты по данным ЦБ
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string ValuteId { get; set; } = string.Empty;

        /// <summary>
        /// Имя валюты на русском языке
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Текущий курс 1 рубля к этой валюте
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Дата на которую действует валюта
        /// </summary>
        [Required]
        public DateTime EffectiveDate { get; set; }
    }
}
