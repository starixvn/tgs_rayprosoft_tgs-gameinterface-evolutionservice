namespace Tgs.GameInterface.EvolutionService.Core.Models
{
    public record GameUrlResponse
    {
        public string? Entry { get; set; }
        public string? EntryEmbedded { get; set; }
        public Error[]? Errors { get; set; }
    }
}
