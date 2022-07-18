using System.Runtime.Serialization;

namespace Tgs.GameInterface.EvolutionService.Grpc.Models;

[DataContract]
public class GetGameUrlRequest
{
    [DataMember(Order = 1)]
    public Uri Ua2AuthenticationUrl { get; set; } = default!;

    [DataMember(Order = 2)]
    public Uri ApiUrl { get; set; } = default!;

    [DataMember(Order = 3)]
    public string PlayerCode { get; set; } = default!;

    [DataMember(Order = 4)]
    public short GameType { get; set; }

    [DataMember(Order = 5)]
    public short PlatformType { get; set; }

    [DataMember(Order = 6)]
    public string IpAddress { get; set; } = default!;

    [DataMember(Order = 7)]
    public string EncryptedToken { get; set; } = default!;

    [DataMember(Order = 8)]
    public string ExternalCultureCode { get; set; } = default!;

    [DataMember(Order = 9)]
    public string ExternalCurrencyCode { get; set; } = default!;

    [DataMember(Order = 10)]
    public string ExternalGameId { get; set; } = default!;
}