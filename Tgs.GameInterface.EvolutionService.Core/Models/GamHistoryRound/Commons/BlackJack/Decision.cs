using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.BlackJack
{
    public record Decision
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
