namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetGameHistoriesQuery(
    Uri ApiUrl,
    string CasinoId,
    string GameHistoryApiToken,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);
