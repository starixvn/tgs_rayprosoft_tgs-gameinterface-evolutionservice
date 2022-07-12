using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound
{
    public record GamHistoryRoundDataDealer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("uid")]
        public string Uid { get; set; }
    }
}
