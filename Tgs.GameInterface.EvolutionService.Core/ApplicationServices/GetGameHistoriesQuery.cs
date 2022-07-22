namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetGameHistoriesQuery(
    Uri ApiUrl,
    string casinoId,
    string gameHistoryApiToken,
    DateTimeOffset StartDate, 
    DateTimeOffset EndDate);