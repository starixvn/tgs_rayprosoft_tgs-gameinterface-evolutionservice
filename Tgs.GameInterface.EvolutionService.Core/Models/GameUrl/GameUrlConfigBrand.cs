using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;

public record GameUrlConfigBrand
{
	[JsonPropertyName("id")]
	public string? Id { get; set; }
	[JsonPropertyName("skin")]
	public string? Skin { get; set; }
}
