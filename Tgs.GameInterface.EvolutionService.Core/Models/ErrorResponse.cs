namespace Tgs.GameInterface.EvolutionService.Core.Models
{
    public record ErrorResponse
    {
        public string Code { get; init; } = default!;
        public string Message { get; init; } = default!;
    }
}
