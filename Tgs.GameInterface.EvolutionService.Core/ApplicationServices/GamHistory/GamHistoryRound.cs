using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;

public record GamHistoryRound
{
	[JsonPropertyName("games")]
	public IEnumerable<GamHistoryRoundData> Games { get; set; } = default!;
}
