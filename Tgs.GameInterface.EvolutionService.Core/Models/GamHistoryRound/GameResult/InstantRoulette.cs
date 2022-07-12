using System.Text.Json.Serialization;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record InstantRoulette
    {
        [JsonPropertyName("outcomes")]
        public InstantRouletteOutcome[] Outcomes { get; set; }
    }
}
