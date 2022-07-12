namespace Tgs.GameInterface.EvolutionService.Client.Models;

public record FundOutRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode,
    string TransactionId,
    decimal Amount);