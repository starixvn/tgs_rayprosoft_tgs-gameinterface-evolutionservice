using Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;

namespace Tgs.GameInterface.EvolutionService.Core.Models;

public record GameUrlRequest
{
    public string Uuid { get; set; } = default!;
    public GameUrlPlayer Player { get; set; } = default!;
    public GameUrlConfig Config { get; set; } = default!;
}
