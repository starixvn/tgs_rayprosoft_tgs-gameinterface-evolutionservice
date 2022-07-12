using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record FanTan
    {
        [JsonPropertyName("buttonsCount")]
        public int ButtonsCount { get; set; }
    }
}
