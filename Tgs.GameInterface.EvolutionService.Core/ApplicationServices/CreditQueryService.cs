using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class CreditQueryService(IGameApiClient client) : IGetService<CreditQuery, CreditResult>
{
    public async ValueTask<CreditResult> GetAsync(CreditQuery query, CancellationToken cancellationToken = default)
    {
        var result = await client.CreditAsync(
            query.ApiUrl,
            "ECR",
            query.AgentId,
            query.PlayerCode,
            query.Amount,
            query.TransactionId,
            "Y",
            cancellationToken).ConfigureAwait(false);

        return new(
            result.Result == "Y",
            result.TransId,
            result.ETransId,
            result.EuId,
            result.Uid,
            result.Balance,
            result.ErrorMessage);
    }
}
