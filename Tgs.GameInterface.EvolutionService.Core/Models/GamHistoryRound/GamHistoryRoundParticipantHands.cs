using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound
{
    public record GamHistoryRoundParticipantHands
    {
        [JsonPropertyName("hand1")]
        public GamHistoryRoundParticipantHand Hand1 { get; set; }
    }
}
