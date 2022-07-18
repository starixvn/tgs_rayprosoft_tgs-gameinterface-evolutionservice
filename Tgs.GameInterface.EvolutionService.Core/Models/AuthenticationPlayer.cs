namespace Tgs.GameInterface.EvolutionService.Core.Models
{
    public record AuthenticationPlayer
    {
        public string Id { get; set; } = default!;
        public bool Update { get; set; }
        public string Country { get; set; } = default!;
        public string Language { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public string Session { get; set; } = default!;
    }
}
