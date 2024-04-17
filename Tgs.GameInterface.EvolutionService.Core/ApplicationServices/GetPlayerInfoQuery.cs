namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetPlayerInfoQuery(
	Uri ApiUrl,
	string AgentId,
	string PlayerCode);
