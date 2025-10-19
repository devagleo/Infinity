using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Domain.Entities
{ 
    /// <summary>
    /// Таблица с избранными валютами пользователями
    /// </summary>
    [Table("favorites")]
    public class FavoriteEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RowId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CurrencyId { get; set; }

    }
}
