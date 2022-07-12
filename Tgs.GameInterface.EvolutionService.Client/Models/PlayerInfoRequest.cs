namespace Tgs.GameInterface.EvolutionService.Client.Models;

public record PlayerInfoRequest(
    Uri ApiUrl,
    string AgentId,
    string PlayerCode);