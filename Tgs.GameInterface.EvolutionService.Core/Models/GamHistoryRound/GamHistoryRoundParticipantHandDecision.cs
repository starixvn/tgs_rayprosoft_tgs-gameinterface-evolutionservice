using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound
{
    public record GamHistoryRoundParticipantHandDecision
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
