using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.Craps
{
    public record Roll
    {
        [JsonPropertyName("rollId")]
        public string RollId { get; set; }
        [JsonPropertyName("startedAt")]
        public DateTimeOffset StartedAt { get; set; }
        [JsonPropertyName("result")]
        public Result Result { get; set; }
    }
}
