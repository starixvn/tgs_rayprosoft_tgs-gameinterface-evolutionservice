using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons
{
    public record DragonTigerCard
    {
        [JsonPropertyName("card")]
        public string Card { get; set; }
        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
}
