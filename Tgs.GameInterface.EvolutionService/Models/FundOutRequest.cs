namespace Tgs.GameInterface.EvolutionService.Models;

public record FundOutRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode,
    string TransactionId,
    decimal Amount);