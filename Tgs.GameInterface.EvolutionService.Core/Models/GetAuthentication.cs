namespace Tgs.GameInterface.EvolutionService.Core.Models
{
    public class GetAuthentication
    {
        public string Uuid { get; init; } = default!;
        public AuthenticationPlayer Player { get; init; } = default!;
    }
}
