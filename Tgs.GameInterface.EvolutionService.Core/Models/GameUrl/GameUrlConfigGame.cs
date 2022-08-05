using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl
{
    public record GameUrlConfigGame
    {
        [JsonPropertyName("category")]
        public string? Category { get; set; }
        [JsonPropertyName("interface")]
        public string? @Interface { get; set; }
        [JsonPropertyName("table")]
        public GameUrlConfigGameTable? Table { get; set; }
    }
}
