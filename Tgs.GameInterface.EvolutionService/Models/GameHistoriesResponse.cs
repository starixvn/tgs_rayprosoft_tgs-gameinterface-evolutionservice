namespace Tgs.GameInterface.EvolutionService.Models;

public record GameHistoriesResponse
{
    public IEnumerable<GameHistoryRecordResponse> GameHistoryRecords { get; init; } = default!;
}
