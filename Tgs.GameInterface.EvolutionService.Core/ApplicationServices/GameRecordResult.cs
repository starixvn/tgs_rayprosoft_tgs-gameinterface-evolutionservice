namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GameRecordResult(
	string ExternalRoundId,
	string ExternalGameId,
	string PlayerCode,
	bool IsClosed,
	bool IsCancelled,
	bool IsVoid,
	DateTimeOffset ExternalCreatedOn,
	DateTimeOffset ExternalClosedOn,
	decimal Bet,
	decimal ValidBet,
	decimal Settle,
	decimal Commission,
	decimal Turnover,
	decimal WinLoss,
	string AdditionalData);
