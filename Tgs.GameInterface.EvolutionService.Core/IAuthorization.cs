namespace Tgs.GameInterface.EvolutionService.Core;

public interface IAuthorization
{
    public string GetAuthorization(string casinoId, string gameHistoryApiToken);
}
