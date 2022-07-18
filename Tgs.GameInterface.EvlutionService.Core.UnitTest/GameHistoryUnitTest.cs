using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;
using Tgs.Logging;

namespace Tgs.GameInterface.EvolutionService.Core.UnitTest;

[TestClass]
public class GameHistoryUnitTest
{
    [TestMethod]
    public async Task Can_Get_Game_History()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.Now;
        var endDate = DateTimeOffset.Now;
        var gameType = "slot";
        var channel = "desktop";
        var gameProvider = "evolution";
        var playerCode = "4ucmqplxb029";
        var transactionsId = "transactionsId";
        var owTransactionId = "owTransactionId";
        var rounding = true;

        var response = @"
                {""uuid"":""d7c76edf-6add-40c9-9586-7206c74b4f29"",""timestamp"":""2022-07-15T10:30:31.012583408"",""data"":[{""date"":""2022-07-15"",""games"":[{""id"":""1701f83995cb442d1cd52e50"",""gameProvider"":""evolution"",""startedAt"":""2022-07-15T09:57:44.059Z"",""settledAt"":""2022-07-15T09:58:24.097Z"",""status"":""Resolved"",""gameType"":""roulette"",""table"":{""id"":""vctlz20yfnmp1ylr"",""name"":""Roulette""},""dealer"":{""uid"":""tts0r8a_________"",""name"":""ROB_298""},""currency"":""PHP"",""participants"":[{""casinoId"":""babylonpega20001"",""playerId"":""diyow60001"",""screenName"":""AJ"",""playerGameId"":""1701f83995cb442d1cd52e50-qghycy35kdoad5hk"",""sessionId"":""qghycy35kdoad5hkqiaussyjrdwabj6cd95a737b"",""casinoSessionId"":""sid-A6095C68"",""currency"":""USD"",""bets"":[{""code"":""ROU_Even"",""stake"":2,""payout"":0,""placedOn"":""2022-07-15T09:58:05.255Z"",""description"":""Even"",""transactionId"":""657793650476066479""},{""code"":""ROU_Red"",""stake"":2,""payout"":4,""placedOn"":""2022-07-15T09:58:05.255Z"",""description"":""Red"",""transactionId"":""657793650476066479""},{""code"":""ROU_StreetBet1617And18"",""stake"":2,""payout"":0,""placedOn"":""2022-07-15T09:58:05.255Z"",""description"":""Street bet: 16, 17, and 18"",""transactionId"":""657793650476066479""}],""configOverlays"":[],""playMode"":""RealMoney"",""betCoverage"":{""simple"":0.78},""channel"":""desktop"",""os"":""Windows"",""device"":""Desktop"",""skinId"":""1"",""brandId"":""1""}],""result"":{""outcomes"":[{""number"":""23"",""type"":""Odd"",""color"":""Red""}]},""wager"":338.270127,""payout"":225.513419}]}]}";

        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, "/api/gamehistory/v1/casino/games")
            .WithHeaders("Authorization", $"Basic {authorization}")
            .WithQueryString("startDate", startDate.ToString("u"))
            .WithQueryString("endDate", endDate.ToString("u"))
            .WithQueryString("gameType", gameType)
            .WithQueryString("channel", channel)
            .WithQueryString("gameProvider", gameProvider)
            .WithQueryString("transactionId", transactionsId)
            .WithQueryString("owTransactionId", owTransactionId)
            .WithQueryString("rounding", rounding.ToString())
            .Respond(HttpStatusCode.OK, new StringContent(response, Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var fakeTgsLog = Substitute.For<ITgsLog>();
        var client = new GameApiClient(fakeHttpClient, fakeLog, fakeTgsLog);

        var result = await client.GetGameHistoryAsync(apiUrl, authorization, startDate, endDate, gameType, channel, gameProvider, transactionsId, owTransactionId, rounding,default);

        var jsonResult = JsonSerializer.Deserialize<GamHistoryRoundResponse>(result);

        Assert.IsTrue(jsonResult!.Data.Any());
    }
}