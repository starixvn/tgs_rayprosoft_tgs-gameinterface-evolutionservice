using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record SlotGames
    {
        [JsonPropertyName("link")]
        public string Path { get; set; }
    }
}
