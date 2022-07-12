namespace Tgs.GameInterface.EvolutionService.Core.Models;

public record AuthenticationResult
{
    public string? Entry { get; init; }
    public string? EntryEmbedded { get; init; }
    public IEnumerable<ErrorResponse>? Errors { get; init; }
}