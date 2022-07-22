using Tgs.GameInterface.EvolutionService.Core;

namespace Tgs.GameInterface.EvolutionService.Models;

public record GetGameHistoriesRequest(
    Uri ApiUrl,
    string casinoId,
    string gameHistoryApiToken,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);