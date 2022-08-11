using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class GetPlayerInfoQueryService : IGetService<GetPlayerInfoQuery, PlayerInfoResult>
{
    private readonly IGameApiClient _client;

    public GetPlayerInfoQueryService(IGameApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async ValueTask<PlayerInfoResult> GetAsync(GetPlayerInfoQuery query, CancellationToken cancellationToken = default)
    {
        var result = await _client.GetPlayerInfoAsync(
            query.ApiUrl,
            "GUI",
            query.AgentId,
            query.PlayerCode,
            cancellationToken).ConfigureAwait(false);

        return new(
            result.EmailAddress,
            result.Firstname,
            result.Lastname,
            result.ScreenName,
            result.CountryCode,
            result.EuId,
            result.Uid,
            result.ErrorMessage);
    }
}
