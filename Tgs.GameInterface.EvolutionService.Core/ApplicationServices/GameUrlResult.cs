using Tgs.GameInterface.EvolutionService.Core.Models;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GameUrlResult(
    string? Entry,
    string? EntryEmbedded,
    Error[]? ErrorMessages);
