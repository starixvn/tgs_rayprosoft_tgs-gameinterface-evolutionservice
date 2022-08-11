namespace Tgs.GameInterface.EvolutionService.Models;

public record GetBalanceRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode);
