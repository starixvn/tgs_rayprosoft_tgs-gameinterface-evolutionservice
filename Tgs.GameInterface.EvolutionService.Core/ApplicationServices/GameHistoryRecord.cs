namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices
{
    public record GameHistoryRecord
    {
        public string PlayerCode { get; init; } = default!;
        public string ExternalRoundId { get; init; } = default!;
        public string ExternalGameId { get; init; } = default!;
        public DateTimeOffset ExternalCreatedOn { get; init; }
        public DateTimeOffset ExternalClosedOn { get; init; }
        public decimal BetAmount { get; init; }
        public decimal ValidBetAmount { get; init; }
        public decimal SettleAmount { get; init; }
        public string AdditionalData { get; init; } = default!;
    }
}
