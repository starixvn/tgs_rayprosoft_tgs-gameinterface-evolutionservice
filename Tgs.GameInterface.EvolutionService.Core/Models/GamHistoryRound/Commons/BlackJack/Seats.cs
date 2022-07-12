using System.Text.Json.Serialization;

namespace Tgs.GameInterface.EvolutionService.Core.Models.GamHistoryRound.Commons.BlackJack
{
    public record Seats
    {
        [JsonPropertyName("Seat1")]
        public Seat Seat1 { get; set; }
        [JsonPropertyName("Seat2")]
        public Seat Seat2 { get; set; }
        [JsonPropertyName("Seat3")]
        public Seat Seat3 { get; set; }
        [JsonPropertyName("Seat4")]
        public Seat Seat4 { get; set; }
        [JsonPropertyName("Seat5")]
        public Seat Seat5 { get; set; }
        [JsonPropertyName("Seat6")]
        public Seat Seat6 { get; set; }
        [JsonPropertyName("Seat7")]
        public Seat Seat7 { get; set; }
    }
}
