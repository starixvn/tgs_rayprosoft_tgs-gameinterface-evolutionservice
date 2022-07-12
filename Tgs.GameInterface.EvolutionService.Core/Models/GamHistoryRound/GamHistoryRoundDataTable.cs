using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound
{
    public record GamHistoryRoundDataTable
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
