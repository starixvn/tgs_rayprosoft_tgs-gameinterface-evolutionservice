namespace Tgs.GameInterface.EvolutionService.Models;

public record KickPlayerRequest(
	Uri ApiUrl,
	string CasinoId,
	string ApiToken,
	string Uuid,
	string PlayerCode);
