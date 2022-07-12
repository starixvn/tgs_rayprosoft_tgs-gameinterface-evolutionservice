namespace Tgs.GameInterface.EvolutionService.Client.Models;

public record FundInRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode,
    string TransactionId,
    decimal Amount);