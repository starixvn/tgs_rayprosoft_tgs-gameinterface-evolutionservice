namespace Tgs.GameInterface.EvolutionService.Client.Models;

public record TransactionInfoRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode,
    string TransactionId);