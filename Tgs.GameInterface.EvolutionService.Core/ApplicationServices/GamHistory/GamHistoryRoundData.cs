using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory
{
    public record GamHistoryRoundData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("participants")]
        public GamHistoryRoundParticipant[] Participants { get; set; }
        [JsonPropertyName("settledAt")]
        public DateTime SettledAt { get; set; }
        [JsonPropertyName("startedAt")]
        public DateTime StartedAt { get; set; }
        [JsonPropertyName("table")]
        public GamHistoryRoundDataTable Table { get; set; }
    }
}
