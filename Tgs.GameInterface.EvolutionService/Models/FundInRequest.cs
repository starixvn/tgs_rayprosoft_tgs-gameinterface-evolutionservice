namespace Tgs.GameInterface.EvolutionService.Models;

public record FundInRequest(
	Uri ApiUrl,
	string AgentId,
	string PlayerCode,
	string TransactionId,
	decimal Amount);
