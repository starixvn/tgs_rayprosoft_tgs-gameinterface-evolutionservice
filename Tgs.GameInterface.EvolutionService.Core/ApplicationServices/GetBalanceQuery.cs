namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetBalanceQuery(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode);