using System.Runtime.CompilerServices;
using Tgs.GameInterface.EvolutionService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class GameRecordQueryService(
    IAuthorization authorization,
    IGameApiClient client) : IQueryService<GameRecordQuery, GameRecordResult>
{
    public async IAsyncEnumerable<GameRecordResult> QueryAsync(
        GameRecordQuery query,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var autho = authorization.GetAuthorization(query.CasinoId, query.GameHistoryApiToken);

        var gameHistories = await client
            .GetGameHistoryAsync(
            query.ApiUrl,
            autho,
            query.StartTime,
            query.EndTime,
            cancellationToken)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false)
            ?? Enumerable.Empty<GameHistoryInfo>();

        if (!gameHistories.Any())
        {
            yield break;
        }

        foreach (var gameHistorie in gameHistories)
        {
            var bet = gameHistorie.Bet;
            var validBet = gameHistorie.Bet;
            var settle = gameHistorie.Settle;

            if (gameHistorie.IsVoid)
            {
                validBet = 0;
                settle = 0;
            }
            var winLoss = settle - validBet;

            yield return new GameRecordResult(
                ExternalRoundId: gameHistorie.ExternalRoundId,
                ExternalGameId: gameHistorie.ExternalGameId.ToString(),
                PlayerCode: gameHistorie.PlayerCode,
                IsClosed: true,
                IsCancelled: gameHistorie.IsCancel,
                IsVoid: gameHistorie.IsVoid,
                ExternalCreatedOn: gameHistorie.ExternalCreatedOn,
                ExternalClosedOn: gameHistorie.ExternalClosedOn,
                Bet: bet,
                ValidBet: validBet,
                Settle: settle,
                Commission: 0,
                Turnover: validBet,
                WinLoss: winLoss,
                AdditionalData: gameHistorie.AdditionalData);
        }
    }
}
