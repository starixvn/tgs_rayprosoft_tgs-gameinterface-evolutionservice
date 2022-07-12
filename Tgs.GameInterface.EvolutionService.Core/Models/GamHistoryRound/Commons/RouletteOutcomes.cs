using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons
{
    public record RouletteOutcomes
    {
        [JsonPropertyName("color")]
        public string Color { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
