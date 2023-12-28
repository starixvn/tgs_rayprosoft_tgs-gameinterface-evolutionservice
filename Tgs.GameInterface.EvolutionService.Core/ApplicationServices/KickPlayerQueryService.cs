using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class KickPlayerQueryService(IGameApiClient client) : IGetService<KickPlayerQuery, bool>
{
    public async ValueTask<bool> GetAsync(KickPlayerQuery query, CancellationToken cancellationToken = default)
    {
        return await client.KickPlayerAsync(
            query.ApiUrl,
            query.CasinoId,
            query.ApiToken,
            query.Uuid,
            query.PlayerCode,
            cancellationToken).ConfigureAwait(false);
    }
}
