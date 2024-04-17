namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record BalanceResult(
	bool Result,
	string PlayerCode,
	string Uid,
	decimal TBalance,
	decimal ABalance,
	bool IsPlayerExist,
	string? ErrorMessage);
