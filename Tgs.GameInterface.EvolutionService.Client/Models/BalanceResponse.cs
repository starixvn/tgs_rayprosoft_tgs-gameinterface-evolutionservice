namespace Tgs.GameInterface.EvolutionService.Client.Models;

public record BalanceResponse(
    bool Result,
    string PlayerCode,
    string Uid,
    decimal IncludeBonusBalance,
    decimal NoIncludeBonusBalance,
    string? ErrorMessage);