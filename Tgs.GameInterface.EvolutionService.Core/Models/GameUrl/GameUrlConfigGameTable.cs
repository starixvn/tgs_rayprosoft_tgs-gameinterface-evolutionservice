using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GameUrl
{
    public record GameUrlConfigGameTable
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}
