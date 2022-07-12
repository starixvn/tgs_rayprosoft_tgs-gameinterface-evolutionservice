using System.Text.Json.Serialization;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.Craps;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record Craps
    {
        [JsonPropertyName("rolls")]
        public Roll[] Rolls { get; set; }
    }
}
