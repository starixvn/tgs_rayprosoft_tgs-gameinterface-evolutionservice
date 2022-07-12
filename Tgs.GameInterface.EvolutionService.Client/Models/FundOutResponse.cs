namespace Tgs.GameInterface.EvolutionService.Client.Models;

public record FundOutResponse(
    bool Result,
    string EvoTransactionId,
    string TransactionId,
    string PlayerCode,
    string Uid,
    decimal Balance,
    string? ErrorMessage);