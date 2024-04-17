using System.ComponentModel.DataAnnotations;

namespace Tgs.GameInterface.EvolutionService.Models;

public record GameRecordRequest
{
	[Required]
	public Uri ApiUrl { get; init; } = default!;
	[Required]
	public string CasinoId { get; init; } = default!;
	[Required]
	public string GameHistoryApiToken { get; init; } = default!;
	[Required]
	public DateTimeOffset StartTime { get; init; }
	[Required]
	public DateTimeOffset EndTime { get; init; }
}
