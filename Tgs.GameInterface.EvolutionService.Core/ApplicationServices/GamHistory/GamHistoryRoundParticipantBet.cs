using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;

public record GamHistoryRoundParticipantBet
{
	[JsonPropertyName("payout")]
	public decimal Payout { get; set; }
	[JsonPropertyName("stake")]
	public decimal Stake { get; set; }
}
