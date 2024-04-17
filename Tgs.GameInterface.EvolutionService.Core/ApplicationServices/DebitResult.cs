namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record DebitResult(
	bool Result,
	string EvoTransactionId,
	string TransactionId,
	string PlayerCode,
	string Uid,
	decimal Balance,
	string? ErrorMessage);
