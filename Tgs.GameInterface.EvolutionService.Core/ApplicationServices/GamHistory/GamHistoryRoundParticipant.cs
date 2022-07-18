using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory
{
    public record GamHistoryRoundParticipant
    {
        [JsonPropertyName("bets")]
        public GamHistoryRoundParticipantBet[] Bets { get; set; }
        [JsonPropertyName("playerId")]
        public string PlayerId { get; set; }
    }
}
