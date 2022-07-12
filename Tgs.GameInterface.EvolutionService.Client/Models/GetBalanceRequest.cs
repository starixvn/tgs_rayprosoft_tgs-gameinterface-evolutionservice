namespace Tgs.GameInterface.EvolutionService.Client.Models;

public record GetBalanceRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode);