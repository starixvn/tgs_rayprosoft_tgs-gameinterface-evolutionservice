using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class GetTransactionInfoQueryService(IGameApiClient client) : IGetService<GetTransactionInfoQuery, TransactionInfoResult>
{
    public async ValueTask<TransactionInfoResult> GetAsync(GetTransactionInfoQuery query, CancellationToken cancellationToken = default)
    {
        var result = await client.GetTransactionInfoAsync(
            query.ApiUrl,
            "TRI",
            query.AgentId,
            query.PlayerCode,
            query.TransactionId,
            cancellationToken).ConfigureAwait(false);

        return new TransactionInfoResult(
            result.Result == "Y",
            result.Balance,
            result.ETransId,
            result.TransId,
            result.Datetime,
            result.DatetimeISO,
            result.Uid,
            result.EuId,
            result.CCode,
            result.Amount,
            result.ErrorMessage);
    }
}
