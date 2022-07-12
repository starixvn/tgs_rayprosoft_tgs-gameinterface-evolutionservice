using Tgs.GameInterface.EvolutionService.Core.Models;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GameUrlResult(
    string? GameUrl,
    Error[]? ErrorMessages);