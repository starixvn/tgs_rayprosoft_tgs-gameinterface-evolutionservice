using System.Text.Json.Nodes;
using NSubstitute;
using Tgs.GameInterface.EvolutionService.Core.ApplicationServices;
using Tgs.GameInterface.EvolutionService.Core.Models;

namespace Tgs.GameInterface.EvolutionService.Core.UnitTest.ApplicationServices;

public class GameRecordQueryServiceUnitTest
{
    [Fact]
    public async Task Status_Is_Cancelled()
    {
        var fakeClient = Substitute.For<IGameApiClient>();
        var fakeAuthorization = Substitute.For<IAuthorization>();

        var query = new GameRecordQuery(
            new Uri("http://localhost"),
            "CasinoId",
            "GameHistoryApiToken",
            DateTimeOffset.Parse("2023-07-29T16:17:00+08:00"),
            DateTimeOffset.Parse("2023-07-29T16:18:00+08:00"));

        var authorization = "authorization";
        _ = fakeAuthorization
            .GetAuthorization(
            Arg.Is(query.CasinoId),
            Arg.Is(query.GameHistoryApiToken))
            .Returns(authorization);

        var resp = new GameHistoryInfo[]{
            new(
                ExternalRoundId: "177648ae4852083a345a4bc6",
                ExternalGameId: "pwsaqk24fcz5qpcr",
                PlayerCode: "4ui0010qu2m0",
                IsCancel:false,
                IsVoid: true,
                ExternalCreatedOn: DateTimeOffset.Parse("2023-07-29T08:17:03.501Z"),
                ExternalClosedOn: DateTimeOffset.Parse("2023-07-29T08:17:58.979Z"),
                Bet: 100,
                Settle: 100,
                AdditionalData: new JsonObject
                {
                    ["id"] = "177648ae4852083a345a4bc6"
                }.ToJsonString())
        };
        _ = fakeClient.GetGameHistoryAsync(
            Arg.Is(query.ApiUrl),
            Arg.Is(authorization),
            Arg.Is(query.StartTime),
            Arg.Is(query.EndTime),
            Arg.Any<CancellationToken>())
            .Returns(resp.ToAsyncEnumerable());

        var service = new GameRecordQueryService(
            fakeAuthorization,
            fakeClient);

        var actual = await service
            .QueryAsync(
            query)
            .ToArrayAsync()
            ?? Enumerable.Empty<GameRecordResult>();

        Assert.NotNull(actual);
        Assert.Equal("177648ae4852083a345a4bc6", actual.ElementAt(0).ExternalRoundId);
        Assert.Equal("pwsaqk24fcz5qpcr", actual.ElementAt(0).ExternalGameId);
        Assert.Equal("4ui0010qu2m0", actual.ElementAt(0).PlayerCode);
        Assert.True(actual.ElementAt(0).IsClosed);
        Assert.False(actual.ElementAt(0).IsCancelled);
        Assert.True(actual.ElementAt(0).IsVoid);
        Assert.Equal(DateTimeOffset.Parse("2023-07-29T08:17:03.501Z"), actual.ElementAt(0).ExternalCreatedOn);
        Assert.Equal(DateTimeOffset.Parse("2023-07-29T08:17:58.979Z"), actual.ElementAt(0).ExternalClosedOn);
        Assert.Equal(100, actual.ElementAt(0).Bet);
        Assert.Equal(0, actual.ElementAt(0).ValidBet);
        Assert.Equal(0, actual.ElementAt(0).Settle);
        Assert.Equal(0, actual.ElementAt(0).Commission);
        Assert.Equal(0, actual.ElementAt(0).Turnover);
        Assert.Equal(0, actual.ElementAt(0).WinLoss);
        Assert.Equal(resp.ElementAt(0).AdditionalData, actual.ElementAt(0).AdditionalData);
    }

    [Fact]
    public async Task Status_Is_Resolved_Player_Has_Removed_Their_Bet()
    {
        var fakeClient = Substitute.For<IGameApiClient>();
        var fakeAuthorization = Substitute.For<IAuthorization>();

        var query = new GameRecordQuery(
            new Uri("http://localhost"),
            "CasinoId",
            "GameHistoryApiToken",
            DateTimeOffset.Parse("2023-07-10T21:22:00+08:00"),
            DateTimeOffset.Parse("2023-07-10T21:25:00+08:00"));

        var authorization = "authorization";
        _ = fakeAuthorization
            .GetAuthorization(
            Arg.Is(query.CasinoId),
            Arg.Is(query.GameHistoryApiToken))
            .Returns(authorization);

        var resp = new GameHistoryInfo[]{
            new(
                ExternalRoundId: "1770845006be6caf347bdd1f",
                ExternalGameId: "Craps00000000001",
                PlayerCode: "4ugqv6fk4iqw",
                IsCancel:true,
                IsVoid: false,
                ExternalCreatedOn: DateTimeOffset.Parse("2023-07-10T13:22:19.504Z"),
                ExternalClosedOn: DateTimeOffset.Parse("2023-07-10T13:24:25.116Z"),
                Bet: 0,
                Settle: 0,
                AdditionalData: new JsonObject
                {
                    ["id"] = "1770845006be6caf347bdd1f"
                }.ToJsonString())
        };
        _ = fakeClient.GetGameHistoryAsync(
            Arg.Is(query.ApiUrl),
            Arg.Is(authorization),
            Arg.Is(query.StartTime),
            Arg.Is(query.EndTime),
            Arg.Any<CancellationToken>())
            .Returns(resp.ToAsyncEnumerable());

        var service = new GameRecordQueryService(
            fakeAuthorization,
            fakeClient);

        var actual = await service
            .QueryAsync(
            query)
            .ToArrayAsync()
            ?? Enumerable.Empty<GameRecordResult>();

        Assert.NotNull(actual);
        Assert.Equal("1770845006be6caf347bdd1f", actual.ElementAt(0).ExternalRoundId);
        Assert.Equal("Craps00000000001", actual.ElementAt(0).ExternalGameId);
        Assert.Equal("4ugqv6fk4iqw", actual.ElementAt(0).PlayerCode);
        Assert.True(actual.ElementAt(0).IsClosed);
        Assert.True(actual.ElementAt(0).IsCancelled);
        Assert.False(actual.ElementAt(0).IsVoid);
        Assert.Equal(DateTimeOffset.Parse("2023-07-10T13:22:19.504Z"), actual.ElementAt(0).ExternalCreatedOn);
        Assert.Equal(DateTimeOffset.Parse("2023-07-10T13:24:25.116Z"), actual.ElementAt(0).ExternalClosedOn);
        Assert.Equal(0, actual.ElementAt(0).Bet);
        Assert.Equal(0, actual.ElementAt(0).ValidBet);
        Assert.Equal(0, actual.ElementAt(0).Settle);
        Assert.Equal(0, actual.ElementAt(0).Commission);
        Assert.Equal(0, actual.ElementAt(0).Turnover);
        Assert.Equal(0, actual.ElementAt(0).WinLoss);
        Assert.Equal(resp.ElementAt(0).AdditionalData, actual.ElementAt(0).AdditionalData);
    }
}
