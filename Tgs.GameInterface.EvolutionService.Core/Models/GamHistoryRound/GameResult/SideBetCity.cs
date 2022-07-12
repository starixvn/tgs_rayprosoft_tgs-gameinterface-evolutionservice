using System.Text.Json.Serialization;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.SideBetCity;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record SideBetCity
    {
        [JsonPropertyName("cardsDealt")]
        public string[] CardsDealt { get; set; }
        [JsonPropertyName("outcome")]
        public SideBetCityOutcome Outcome { get; set; }
    }
}
