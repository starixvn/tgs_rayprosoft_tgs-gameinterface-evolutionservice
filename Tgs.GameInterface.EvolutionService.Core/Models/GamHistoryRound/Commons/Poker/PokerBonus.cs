using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.Poker
{
    public record PokerBonus
    {
        [JsonPropertyName("tripsBonus")]
        public PokerBonusTripsbonus TripsBonus { get; set; }
    }
}
