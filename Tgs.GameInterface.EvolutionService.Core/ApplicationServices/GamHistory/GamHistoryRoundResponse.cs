using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;

public record GamHistoryRoundResponse
{
    [JsonPropertyName("data")]
    public IEnumerable<GamHistoryRound> Data { get; set; } = default!;
}
