using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound
{
    public record GamHistoryRoundData
    {
        [JsonPropertyName("dealer")]
        public GamHistoryRoundDataDealer Dealer { get; set; }
        [JsonPropertyName("gameSubType")]
        public string GameSubType { get; set; }
        [JsonPropertyName("gameType")]
        public string GameType { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("gameProvider")]
        public string GameProvider { get; set; }
        [JsonPropertyName("participants")]
        public GamHistoryRoundParticipant[] Participants { get; set; }
        [JsonPropertyName("result")]
        public JsonDocument Result { get; set; }
        [JsonPropertyName("settledAt")]
        public DateTime SettledAt { get; set; }
        [JsonPropertyName("startedAt")]
        public DateTime StartedAt { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("table")]
        public GamHistoryRoundDataTable Table { get; set; }
    }
}
