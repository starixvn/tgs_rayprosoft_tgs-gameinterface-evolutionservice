namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GameRecordQuery(
	Uri ApiUrl,
	string CasinoId,
	string GameHistoryApiToken,
	DateTimeOffset StartTime,
	DateTimeOffset EndTime);
