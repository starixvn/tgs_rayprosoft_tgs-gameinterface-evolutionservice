using Tgs.GameInterface.EvolutionService.Core.Models;

namespace Tgs.GameInterface.EvolutionService.Models;

public record GameUrlResponse(
    string? GameUrl,
    Error[]? ErrorMessages);