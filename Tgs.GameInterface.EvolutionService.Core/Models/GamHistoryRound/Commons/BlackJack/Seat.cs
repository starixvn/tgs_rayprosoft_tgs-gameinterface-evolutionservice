using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.BlackJack
{
    public record Seat
    {
        [JsonPropertyName("score")]
        public int Score { get; set; }
        [JsonPropertyName("cards")]
        public string[] Cards { get; set; }
        [JsonPropertyName("decisions")]
        public Decision[] Decisions { get; set; }
        [JsonPropertyName("outcome")]
        public string Outcome { get; set; }
        [JsonPropertyName("bonusCards")]
        public object[] BonusCards { get; set; }
    }
}
