using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;

public record GamHistoryRoundData
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = default!;
	[JsonPropertyName("status")]
	public string Status { get; set; } = default!;
	[JsonPropertyName("participants")]
	public GamHistoryRoundParticipant[] Participants { get; set; } = default!;
	[JsonPropertyName("settledAt")]
	public DateTime SettledAt { get; set; }
	[JsonPropertyName("startedAt")]
	public DateTime StartedAt { get; set; }
	[JsonPropertyName("table")]
	public GamHistoryRoundDataTable Table { get; set; } = default!;
	[JsonPropertyName("result")]
	public JsonDocument Result { get; set; } = default!;
}
