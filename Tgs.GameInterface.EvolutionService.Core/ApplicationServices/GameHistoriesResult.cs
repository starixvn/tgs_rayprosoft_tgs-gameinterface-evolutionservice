namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GameHistoriesResult
{
	public IEnumerable<GameHistoryRecord> GameHistoryRecords { get; init; } = default!;
}
