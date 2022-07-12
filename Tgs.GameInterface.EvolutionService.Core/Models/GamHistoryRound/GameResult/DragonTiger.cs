using System.Text.Json.Serialization;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record DragonTiger
    {
        [JsonPropertyName("dragon")]
        public DragonTigerCard Dragon { get; set; }
        [JsonPropertyName("outcome")]
        public string Outcome { get; set; }
        [JsonPropertyName("tiger")]
        public DragonTigerCard Tiger { get; set; }
    }
}
