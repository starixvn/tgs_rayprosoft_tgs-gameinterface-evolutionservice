using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.Craps
{
    public record Result
    {
        [JsonPropertyName("first")]
        public int First { get; set; }
        [JsonPropertyName("second")]
        public int Second { get; set; }
    }
}
