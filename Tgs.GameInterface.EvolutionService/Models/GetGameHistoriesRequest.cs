using Tgs.GameInterface.EvolutionService.Core;

namespace Tgs.GameInterface.EvolutionService.Models;

public record GetGameHistoriesRequest(
    Uri ApiUrl,
    string Authorization,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);