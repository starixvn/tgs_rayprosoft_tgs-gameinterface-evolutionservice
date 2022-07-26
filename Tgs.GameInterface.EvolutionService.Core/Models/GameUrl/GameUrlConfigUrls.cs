using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl
{
    public record GameUrlConfigUrls
    {
        [JsonPropertyName("cashier")]
        public string? Cashier { get; set; }
        [JsonPropertyName("responsibleGaming")]
        public string? ResponsibleGaming { get; set; }
        [JsonPropertyName("lobby")]
        public string? Lobby { get; set; }
        [JsonPropertyName("sessionTimeout")]
        public string? SessionTimeout { get; set; }
    }
}
