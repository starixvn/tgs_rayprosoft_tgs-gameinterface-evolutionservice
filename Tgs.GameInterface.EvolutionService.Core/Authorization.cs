using System.Text;

namespace Tgs.GameInterface.EvolutionService.Core;

internal class Authorization : IAuthorization
{
    public string GetAuthorization(string casinoId, string gameHistoryApiToken)
        => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{casinoId}:{gameHistoryApiToken}"));
}
