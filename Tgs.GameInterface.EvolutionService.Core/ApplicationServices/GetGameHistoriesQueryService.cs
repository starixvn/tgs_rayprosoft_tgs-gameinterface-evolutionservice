using System.Text;
using System.Text.Json;
using Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class GetGameHistoriesQueryService : IGetService<GetGameHistoriesQuery, GameHistoriesResult>
{
    private readonly IGameApiClient _client;

    public GetGameHistoriesQueryService(IGameApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async ValueTask<GameHistoriesResult> GetAsync(GetGameHistoriesQuery query, CancellationToken cancellationToken = default)
    {
        var authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{query.casinoId}:{query.gameHistoryApiToken}"));

        var result = await _client.GetGameHistoryAsync(
            query.ApiUrl,
            authorization,
            query.StartDate,
            query.EndDate,
            null,
            null,
            null,
            null,
            null,
            null,
            cancellationToken).ConfigureAwait(false);

        var jsonResult = JsonSerializer.Deserialize<GamHistoryRoundResponse>(result);

        return new GameHistoriesResult
        {
            GameHistoryRecords = jsonResult?.Data.SelectMany(x => x.Games.SelectMany(g => g.Participants.Select(p => new GameHistoryRecord
            {
                PlayerCode = p.PlayerId,
                ExternalRoundId = g.Id,
                ExternalGameId = g.Table.Id,
                ExternalCreatedOn = g.StartedAt,
                ExternalClosedOn = g.SettledAt,
                BetAmount = p.Bets.Sum(b => b.Stake),
                ValidBetAmount = p.Bets.Sum(b => b.Stake),
                SettleAmount = p.Bets.Sum(b => b.Payout),
                AdditionalData = JsonSerializer.Serialize(g.Result)
            }))) ?? Array.Empty<GameHistoryRecord>()
        };
    }
}
