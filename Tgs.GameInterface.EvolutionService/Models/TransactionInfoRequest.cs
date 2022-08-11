namespace Tgs.GameInterface.EvolutionService.Models;

public record TransactionInfoRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode,
    string TransactionId);
