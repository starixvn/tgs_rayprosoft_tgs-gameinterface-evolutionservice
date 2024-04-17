using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;

public record GamHistoryRoundParticipant
{
	[JsonPropertyName("bets")]
	public IEnumerable<GamHistoryRoundParticipantBet> Bets { get; set; } = default!;
	[JsonPropertyName("playerId")]
	public string PlayerId { get; set; } = default!;
}
