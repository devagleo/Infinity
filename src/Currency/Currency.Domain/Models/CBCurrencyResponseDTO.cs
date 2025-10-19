using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Currency.Domain.Models
{
    [XmlRoot("ValCurs")]
    public record CBCurrencyResponseDTO
    {
        [XmlAttribute("Date")]
        public string Date { get; set; } = string.Empty;

        [XmlElement("Valute")]
        public List<CBCurrencyItem> ValuteItems { get; set; } = new();
    }
}
