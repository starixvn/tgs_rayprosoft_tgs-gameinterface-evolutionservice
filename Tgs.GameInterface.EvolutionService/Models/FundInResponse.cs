namespace Tgs.GameInterface.EvolutionService.Models;

public record FundInResponse(
    bool Result,
    string EvoTransactionId,
    string TransactionId,
    string PlayerCode,
    string Uid,
    decimal Balance,
    string? ErrorMessage);
