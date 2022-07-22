namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetGameHistoriesQuery(
    Uri ApiUrl,
    string Authorization,
    DateTimeOffset StartDate, 
    DateTimeOffset EndDate);