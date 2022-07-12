using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons
{
    public record InstantRouletteOutcome
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("wheel")]
        public string Wheel { get; set; }
        [JsonPropertyName("spin")]
        public string Spin { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; }
    }
}
