using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound
{
    public record GamHistoryRoundParticipantHand
    {
        [JsonPropertyName("cards")]
        public string[] Cards { get; set; }
        [JsonPropertyName("decisions")]
        public GamHistoryRoundParticipantHandDecision[] Decisions { get; set; }
        [JsonPropertyName("outcome")]
        public string Outcome { get; set; }
        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
}
