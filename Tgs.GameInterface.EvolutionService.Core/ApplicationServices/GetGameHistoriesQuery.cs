namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetGameHistoriesQuery(
    Uri ApiUrl,
    string Authorization,
    DateTimeOffset? StartDate, 
    DateTimeOffset? EndDate, 
    string? GameType, 
    string? Channel, 
    string? GameProvider, 
    string? TransactionId, 
    string? OwTransactionId, 
    bool? Rounding);