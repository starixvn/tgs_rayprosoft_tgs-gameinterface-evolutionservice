namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record KickPlayerQuery(
    Uri ApiUrl,
    string CasinoId, 
    string ApiToken,
    string Uuid,
    string PlayerCode);