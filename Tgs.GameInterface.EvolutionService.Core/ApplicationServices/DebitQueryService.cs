using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class DebitQueryService(IGameApiClient client) : IGetService<DebitQuery, DebitResult>
{
	public async ValueTask<DebitResult> GetAsync(DebitQuery query, CancellationToken cancellationToken = default)
	{
		var result = await client.DebitAsync(
			query.ApiUrl,
			"EDB",
			query.AgentId,
			query.PlayerCode,
			query.Amount,
			query.TransactionId,
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
