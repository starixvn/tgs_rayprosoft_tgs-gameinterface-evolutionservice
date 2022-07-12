using System.Text.Json.Serialization;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.BlackJack;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record BlackJack
    {
        [JsonPropertyName("dealer")]
        public Dealer Dealer { get; set; }
        [JsonPropertyName("seats")]
        public Seats Seats { get; set; }
        [JsonPropertyName("burnedCards")]
        public object[] BurnedCards { get; set; }
    }
}
