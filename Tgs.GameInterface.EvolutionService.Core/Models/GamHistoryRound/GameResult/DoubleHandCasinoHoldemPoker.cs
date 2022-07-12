using System.Text.Json.Serialization;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.DoubleHandCasinoHoldemPoker;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.Poker;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record DoubleHandCasinoHoldemPoker
    {
        [JsonPropertyName("dealer")]
        public PokerDealer Dealer { get; set; }
        [JsonPropertyName("cards")]
        public PokerCards Cards { get; set; }
        [JsonPropertyName("firstHand")]
        public DoubleHandCasinoHoldemPokerHand FirstHand { get; set; }
        [JsonPropertyName("secondHand")]
        public DoubleHandCasinoHoldemPokerHand SecondHand { get; set; }
    }
}
