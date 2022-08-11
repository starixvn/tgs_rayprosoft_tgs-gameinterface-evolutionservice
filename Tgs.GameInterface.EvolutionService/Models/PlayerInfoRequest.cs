namespace Tgs.GameInterface.EvolutionService.Models;

public record PlayerInfoRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode);
