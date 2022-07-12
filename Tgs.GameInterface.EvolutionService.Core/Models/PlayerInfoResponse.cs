using System.Xml.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models;

[XmlRoot("userdetails")]
public class PlayerInfoResponse
{
    [XmlElement("emailaddress")]
    public string EmailAddress { get; init; } = default!;
    [XmlElement("firstname")]
    public string Firstname { get; init; } = default!;
    [XmlElement("lastname")]
    public string Lastname { get; init; } = default!;
    [XmlElement("screenname")]
    public string ScreenName { get; init; } = default!;
    [XmlElement("countrycode")]
    public string CountryCode { get; init; } = default!;
    [XmlElement("euid")]
    public string EuId { get; init; } = default!;
    [XmlElement("uid")]
    public string Uid { get; init; } = default!;
    [XmlElement("errormsg")]
    public string? ErrorMessage { get; init; }
}