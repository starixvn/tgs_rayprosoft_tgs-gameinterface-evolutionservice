namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record TransactionInfoResult(
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
