using System.Xml.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models
{
    [XmlRoot("userbalance")]
    public record BalanceResponse
    {
        [XmlElement("result")]
        public string Result { get; init; } = default!;
        [XmlElement("tbalance")]
        public decimal TBalance { get; init; }
        [XmlElement("abalance")]
        public decimal ABalance { get; init; }
        [XmlElement("euid")]
        public string PlayerCode { get; init; } = default!;
        [XmlElement("uid")]
        public string Uid { get; init; } = default!;
        [XmlElement("errormsg")]
        public string? ErrorMessage { get; init; }
    }
}
