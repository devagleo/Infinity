using System.Globalization;
using System.Xml.Serialization;
using Shared.Helpers;

namespace Currency.Domain.Models
{
    /// <summary>
    /// DTO для обработки XML данных с сайта ЦБ
    /// Конкретный класс валюты
    /// </summary>
    public class CBCurrencyItem
    {
        /// <summary>
        /// ID валюты по версии ЦБ
        /// </summary>
        [XmlAttribute("ID")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Имя валюты на русском языке
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Курс валюты к 1 рублю в формате с плавающей точкой в Decimal
        /// </summary>
        [XmlIgnore]
        public decimal VunitRate { get; set; }

        /// <summary>
        /// Поле для сериализации стринга к Decimal
        /// </summary>
        [XmlElement("VunitRate")]
        public string VunitRateRaw
        {
            get => VunitRate.ToString(CultureInfo.InvariantCulture);
            set
            {
                VunitRate = DecimalParser.ParseCurrencyValue(value);
            }
        }
    }
}
