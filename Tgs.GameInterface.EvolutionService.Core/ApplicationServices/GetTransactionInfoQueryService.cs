using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class GetTransactionInfoQueryService : IGetService<GetTransactionInfoQuery, TransactionInfoResult>
{
    private readonly IGameApiClient _client;

    public GetTransactionInfoQueryService(IGameApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async ValueTask<TransactionInfoResult> GetAsync(GetTransactionInfoQuery query, CancellationToken cancellationToken = default)
    {
        var result = await _client.GetTransactionInfoAsync(
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
