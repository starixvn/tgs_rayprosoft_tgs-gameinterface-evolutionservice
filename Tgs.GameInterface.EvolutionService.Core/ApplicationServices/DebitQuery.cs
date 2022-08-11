namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record DebitQuery(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode,
    string TransactionId,
    decimal Amount);
