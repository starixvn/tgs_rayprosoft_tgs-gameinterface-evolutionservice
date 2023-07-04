using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;

public record GameUrlConfigChannel
{
    [JsonPropertyName("wrapped")]
    public bool Wrapped { get; set; }
    [JsonPropertyName("mobile")]
    public bool Mobile { get; set; }
}
