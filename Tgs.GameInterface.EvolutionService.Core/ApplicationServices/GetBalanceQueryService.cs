using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class GetBalanceQueryService(IGameApiClient client) : IGetService<GetBalanceQuery, BalanceResult>
{
    public async ValueTask<BalanceResult> GetAsync(GetBalanceQuery query, CancellationToken cancellationToken = default)
    {
        var result = await client.GetBalanceAsync(
            query.ApiUrl,
            "RWA",
            query.AgentId,
            query.PlayerCode,
            cancellationToken).ConfigureAwait(false);

        return new BalanceResult(
             result.Result == "Y",
             result.PlayerCode,
             result.Uid,
             TBalance: result.TBalance,
             ABalance: result.ABalance,
             result.Result == "Y" && !string.IsNullOrEmpty(result.PlayerCode),
             ErrorMessage: result.ErrorMessage);
    }
}
