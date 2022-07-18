namespace Tgs.GameInterface.EvolutionService.Models;

public record BalanceResponse(
    bool Result,
    string PlayerCode,
    string Uid,
    decimal? TBalance,
    decimal? ABalance,
    string? ErrorMessage);