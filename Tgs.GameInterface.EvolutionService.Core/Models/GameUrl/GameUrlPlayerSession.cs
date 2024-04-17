using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;

public record GameUrlPlayerSession
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = default!;
	[JsonPropertyName("ip")]
	public string Ip { get; set; } = default!;
}
