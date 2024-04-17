namespace Tgs.GameInterface.EvolutionService.Models;

public record TransactionInfoResponse(
	bool Result,
	decimal Balance,
	string TransactionId,
	string EvoTransactionId,
	DateTime CreatedOn,
	DateTimeOffset CreatedOnISO,
	string EvoUid,
	string PlayerCode,
	string CCid,
	decimal Amount,
	string? ErrorMessage);
