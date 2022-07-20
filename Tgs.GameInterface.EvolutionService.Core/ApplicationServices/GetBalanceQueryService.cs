using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices
{
    internal class GetBalanceQueryService : IGetService<GetBalanceQuery, BalanceResult>
    {
        private readonly IGameApiClient _client;

        public GetBalanceQueryService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask<BalanceResult> GetAsync(GetBalanceQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _client.GetBalanceAsync(
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
                 result.Result == "N" && result.ErrorMessage?.Trim().ToLower() == "Invaliduser",
                 ErrorMessage: result.ErrorMessage);
        }
    }
}
