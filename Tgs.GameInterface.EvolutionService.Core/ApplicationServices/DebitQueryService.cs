using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class DebitQueryService : IGetService<DebitQuery, DebitResult>
{
    private readonly IGameApiClient _client;

    public DebitQueryService(IGameApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async ValueTask<DebitResult> GetAsync(DebitQuery query, CancellationToken cancellationToken = default)
    {
        var result = await _client.DebitAsync(
            query.ApiUrl,
            "EDB",
            query.AgentId,
            query.PlayerCode,
            query.Amount,
            query.TransactionId,
            cancellationToken).ConfigureAwait(false);

        return new(
            result.Result == "Y",
            result.ETransId,
            result.TransId,
            result.EuId,
            result.Uid,
            result.Balance,
            result.ErrorMessage);
    }
}