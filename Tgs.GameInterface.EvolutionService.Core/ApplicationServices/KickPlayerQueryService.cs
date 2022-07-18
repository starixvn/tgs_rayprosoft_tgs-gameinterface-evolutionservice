using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class KickPlayerQueryService : IGetService<KickPlayerQuery, bool>
{
    private readonly IGameApiClient _client;

    public KickPlayerQueryService(IGameApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async ValueTask<bool> GetAsync(KickPlayerQuery query, CancellationToken cancellationToken = default)
    {
        return await _client.KickPlayerAsync(
            query.ApiUrl,
            query.CasinoId,
            query.ApiToken,
            query.Uuid,
            query.PlayerCode,
            cancellationToken).ConfigureAwait(false);
    }
}