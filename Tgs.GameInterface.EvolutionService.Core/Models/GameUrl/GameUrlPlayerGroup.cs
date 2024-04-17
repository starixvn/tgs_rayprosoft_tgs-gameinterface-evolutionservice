using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;

public record GameUrlPlayerGroup
{
	[JsonPropertyName("id")]
	public string? Id { get; set; }
	[JsonPropertyName("action")]
	public string Action { get; set; } = default!;
}
