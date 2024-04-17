using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;

public record GameUrlConfig
{
	[JsonPropertyName("brand")]
	public GameUrlConfigBrand? Brand { get; set; }
	[JsonPropertyName("game")]
	public GameUrlConfigGame? Game { get; set; }
	[JsonPropertyName("channel")]
	public GameUrlConfigChannel? Channel { get; set; }
	[JsonPropertyName("urls")]
	public GameUrlConfigUrls? Urls { get; set; }
	[JsonPropertyName("freeGames")]
	public bool FreeGames { get; set; }
}
