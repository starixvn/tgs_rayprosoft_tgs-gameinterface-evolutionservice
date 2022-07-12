using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.Poker
{
    public record PokerCards
    {
        [JsonPropertyName("dealer")]
        public string[] Dealer { get; set; }
        [JsonPropertyName("flop")]
        public string[] Flop { get; set; }
        [JsonPropertyName("player")]
        public string[] Player { get; set; }
        [JsonPropertyName("river")]
        public string[] River { get; set; }
    }
}
