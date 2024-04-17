namespace Tgs.GameInterface.EvolutionService.Core.Models;

public record GameHistoryInfo(
	string ExternalRoundId,
	string ExternalGameId,
	string PlayerCode,
	bool IsCancel,
	bool IsVoid,
	DateTimeOffset ExternalCreatedOn,
	DateTimeOffset ExternalClosedOn,
	decimal Bet,
	decimal Settle,
	string AdditionalData);
