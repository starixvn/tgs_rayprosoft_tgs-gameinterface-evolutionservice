using System.Globalization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models;

[XmlRoot("transaction")]
public class TransactionInfoResponse
{
    [XmlElement("result")]
    public string Result { get; init; } = default!;
    [XmlElement("balance")]
    public decimal Balance { get; init; }
    [XmlElement("etransid")]
    public string ETransId { get; init; } = default!;
    [XmlElement("transid")]
    public string TransId { get; init; } = default!;
    [XmlElement("datetime")]
    public string _Datetime { get; init; }
    [XmlIgnore]
    public DateTime Datetime => DateTime.ParseExact(_Datetime, "ddd MMM dd HH:mm:ss UTC yyyy", CultureInfo.InvariantCulture);
    [XmlElement("datetime-iso")]
    public DateTimeOffset DatetimeISO { get; init; }
    [XmlElement("euID")]
    public string EuId { get; init; } = string.Empty;
    [XmlElement("uID")]
    public string Uid { get; init; } = string.Empty;
    [XmlElement("cCode")]
    public string CCode { get; init; } = default!;
    [XmlElement("amount")]
    public decimal Amount { get; init; }
    [XmlElement("errormsg")]
    public string? ErrorMessage { get; init; }
}