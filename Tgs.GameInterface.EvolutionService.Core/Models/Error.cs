namespace Tgs.GameInterface.EvolutionService.Core.Models;

public record Error
{
    public string Code { get; set; } = default!;
    public string Message { get; set; } = default!;
}
