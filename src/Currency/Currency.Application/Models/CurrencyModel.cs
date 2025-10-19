using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Application.Models
{
    /// <summary>
    /// Модель передачи данных между GRPC и Application слоями
    /// </summary>
    public class CurrencyModel
    {
        /// <summary>
        /// ID валюты в базе
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название валюты на русском
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Курс валюты к 1 рублю
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Код валюты
        /// </summary>
        public string ValuteId { get; set; } = string.Empty;

        /// <summary>
        /// Дата на которую действует валюта
        /// </summary>
        public DateTime EffectiveDate { get; set; }
    }
}
