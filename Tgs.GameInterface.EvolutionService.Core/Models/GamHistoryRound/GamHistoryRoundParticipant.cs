using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound
{
    public record GamHistoryRoundParticipant
    {
        [JsonPropertyName("bets")]
        public GamHistoryRoundParticipantBet[] Bets { get; set; }
        [JsonPropertyName("casinoId")]
        public string CasinoId { get; set; }
        [JsonPropertyName("hands")]
        public GamHistoryRoundParticipantHands Hands { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; }
        [JsonPropertyName("configOverlays")]
        public object[] ConfigOverlays { get; set; }
        [JsonPropertyName("playMode")]
        public string playMode { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("device")]
        public string Device { get; set; }
        [JsonPropertyName("os")]
        public string Os { get; set; }
        [JsonPropertyName("playerId")]
        public string PlayerId { get; set; }
        [JsonPropertyName("screenName")]
        public string ScreenName { get; set; }
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }
        [JsonPropertyName("casinoSessionId")]
        public string CasinoSessionId { get; set; }
        [JsonPropertyName("sideBetBankerPair")]
        public string SideBetBankerPair { get; set; }
        [JsonPropertyName("sideBetPlayerPair")]
        public string SideBetPlayerPair { get; set; }
        [JsonPropertyName("subType")]
        public string SubType { get; set; }
    }
}
