using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;

public record GameUrlPlayer
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = default!;
	[JsonPropertyName("update")]
	public bool Update { get; set; }
	[JsonPropertyName("firstName")]
	public string? FirstName { get; set; }
	[JsonPropertyName("lastName")]
	public string? LastName { get; set; }
	[JsonPropertyName("nickname")]
	public string? Nickname { get; set; }
	[JsonPropertyName("country")]
	public string Country { get; set; } = default!;
	[JsonPropertyName("language")]
	public string Language { get; set; } = default!;
	[JsonPropertyName("currency")]
	public string Currency { get; set; } = default!;
	[JsonPropertyName("session")]
	public GameUrlPlayerSession Session { get; set; } = default!;
	[JsonPropertyName("group")]
	public GameUrlPlayerGroup? Group { get; set; }
}
