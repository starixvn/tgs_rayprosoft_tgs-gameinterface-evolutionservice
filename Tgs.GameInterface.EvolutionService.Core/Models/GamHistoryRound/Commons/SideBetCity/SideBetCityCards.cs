using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.SideBetCity
{
    public record SideBetCityCards
    {
        [JsonPropertyName("cards")]
        public string[] Cards { get; set; }
        [JsonPropertyName("handType")]
        public string HandType { get; set; }
    }
}
