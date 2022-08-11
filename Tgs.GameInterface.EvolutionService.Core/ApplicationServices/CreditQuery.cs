namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record CreditQuery(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode,
    string TransactionId,
    decimal Amount);
