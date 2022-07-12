using System.Text.Json.Serialization;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.Poker;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.DoubleHandCasinoHoldemPoker
{
    public record DoubleHandCasinoHoldemPokerHand
    {
        [JsonPropertyName("player")]
        public PokerPlayer Player { get; set; }
        [JsonPropertyName("outcome")]
        public string Outcome { get; set; }
    }
}
