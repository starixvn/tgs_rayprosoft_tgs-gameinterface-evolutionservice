using System.Text.Json.Serialization;
using Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.ScalableBlackJack;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.GameResult
{
    public record ScalableBlackJack
    {
        [JsonPropertyName("dealtToPlayer")]
        public string[] DealtToPlayer { get; set; }
        [JsonPropertyName("dealerHand")]
        public Dealerhand DealerHand { get; set; }
        [JsonPropertyName("wonSideBets")]
        public object[] WonSideBets { get; set; }
    }
}
