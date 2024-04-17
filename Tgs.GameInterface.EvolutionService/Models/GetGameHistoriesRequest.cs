namespace Tgs.GameInterface.EvolutionService.Models;

public record GetGameHistoriesRequest(
	Uri ApiUrl,
	string CasinoId,
	string GameHistoryApiToken,
	DateTimeOffset StartDate,
	DateTimeOffset EndDate);
