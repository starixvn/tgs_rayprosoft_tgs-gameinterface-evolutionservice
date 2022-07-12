namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetTransactionInfoQuery(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode,
    string TransactionId);