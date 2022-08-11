using System.Globalization;
using System.Xml.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models;

[XmlRoot("transfer")]
public class CreditResponse
{
    [XmlElement("datetime")]
    public string _Datetime { get; init; }

    [XmlElement("balance")]
    public decimal Balance { get; init; }

    [XmlIgnore]
    public DateTime Datetime => DateTime.ParseExact(_Datetime, "ddd MMM dd HH:mm:ss UTC yyyy", CultureInfo.InvariantCulture);

    [XmlElement("errormsg")]
    public string? ErrorMessage { get; init; }

    [XmlElement("etransid")]
    public string ETransId { get; init; } = default!;

    [XmlElement("euid")]
    public string EuId { get; init; } = string.Empty;

    [XmlElement("result")]
    public string Result { get; init; } = default!;

    [XmlElement("transid")]
    public string TransId { get; init; } = default!;

    [XmlElement("uid")]
    public string Uid { get; init; } = string.Empty;
}
