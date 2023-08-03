using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;
using Microsoft.Extensions.Logging.Abstractions;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;
using Tgs.GameInterface.EvolutionService.Core.Models;

namespace Tgs.GameInterface.EvolutionService.Core.UnitTest;

public class GameHistoryUnitTest
{
    [Fact]
    public async Task Can_Get_Game_History()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.UtcNow;
        var endDate = DateTimeOffset.UtcNow;
        var gameType = "slot";
        var channel = "desktop";
        var gameProvider = "evolution";
        //var playerCode = "4ucmqplxb029";
        var transactionsId = "transactionsId";
        var owTransactionId = "owTransactionId";
        var rounding = true;

        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["gameType"] = gameType;
        query["channel"] = channel;
        query["gameProvider"] = gameProvider;
        query["transactionId"] = transactionsId;
        query["owTransactionId"] = owTransactionId;
        query["rounding"] = rounding.ToString();
        uriBuilder.Query = query.ToString();

        var response =
            "{\"uuid\":\"d7c76edf-6add-40c9-9586-7206c74b4f29\",\"timestamp\":\"2022-07-15T10:30:31.012583408\",\"data\":[{\"date\":\"2022-07-15\",\"games\":[\n{\"id\":\"1701f83995cb442d1cd52e50\",\"gameProvider\":\"evolution\",\"startedAt\":\"2022-07-15T09:57:44.059Z\",\"settledAt\":\"2022-07-15T09:58:24.097Z\",\"status\":\"Resolved\",\"gameType\":\"roulette\",\"table\":{\"id\":\"vctlz20yfnmp1ylr\",\"name\":\"Roulette\"},\"dealer\":{\"uid\":\"tts0r8a_________\",\"name\":\"ROB_298\"},\"currency\":\"PHP\",\"participants\":[{\"casinoId\":\"babylonpega20001\",\"playerId\":\"diyow60001\",\"screenName\":\"AJ\",\"playerGameId\":\"1701f83995cb442d1cd52e50-qghycy35kdoad5hk\",\"sessionId\":\"qghycy35kdoad5hkqiaussyjrdwabj6cd95a737b\",\"casinoSessionId\":\"sid-A6095C68\",\"currency\":\"USD\",\"bets\":[{\"code\":\"ROU_Even\",\"stake\":2,\"payout\":0,\"placedOn\":\"2022-07-15T09:58:05.255Z\",\"description\":\"Even\",\"transactionId\":\"657793650476066479\"},{\"code\":\"ROU_Red\",\"stake\":2,\"payout\":4,\"placedOn\":\"2022-07-15T09:58:05.255Z\",\"description\":\"Red\",\"transactionId\":\"657793650476066479\"},{\"code\":\"ROU_StreetBet1617And18\",\"stake\":2,\"payout\":0,\"placedOn\":\"2022-07-15T09:58:05.255Z\",\"description\":\"Street bet: 16, 17, and 18\",\"transactionId\":\"657793650476066479\"}],\"configOverlays\":[],\"playMode\":\"RealMoney\",\"betCoverage\":{\"simple\":0.78},\"channel\":\"desktop\",\"os\":\"Windows\",\"device\":\"Desktop\",\"skinId\":\"1\",\"brandId\":\"1\"}],\"result\":{\"outcomes\":[{\"number\":\"23\",\"type\":\"Odd\",\"color\":\"Red\"}]},\"wager\":338.270127,\"payout\":225.513419}\n]}\n]}";

        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, uriBuilder.ToString())
            .WithHeaders("Authorization", $"Basic {authorization}")
            .Respond(HttpStatusCode.OK, new StringContent(response, Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var result = await client.GetGameHistoryAsync(
            apiUrl,
            authorization,
            startDate,
            endDate,
            gameType,
            channel,
            gameProvider,
            transactionsId,
            owTransactionId,
            rounding);

        var jsonResult = JsonSerializer.Deserialize<GamHistoryRoundResponse>(result);

        Assert.True(jsonResult!.Data.Any());
    }

    [Fact]
    public async Task Game_is_BacBo()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.Parse("2023-07-30T23:21:00+08:00");
        var endDate = DateTimeOffset.Parse("2023-07-30T23:22:00+08:00");

        var response = new JsonObject
        {
            ["uuid"] = "528b2739-fc91-4902-9750-ecbf4e2a226a",
            ["timestamp"] = "2023-08-03T02:11:12.531720100",
            ["data"] = new JsonArray
            {
                new JsonObject {
                    ["date"] = "2023-07-30",
                    ["games"] = new JsonArray
                    {
                        new JsonObject {
                            ["id"] = "1776ae69497e716d7b64c0cf",
                            ["gameProvider"] = "evolution",
                            ["startedAt"] = "2023-07-30T15:21:17.352Z",
                            ["settledAt"] = "2023-07-30T15:21:46.138Z",
                            ["status"] = "Resolved",
                            ["gameType"] = "bacbo",
                            ["table"] = new JsonObject
                            {
                                ["id"] = "BacBo00000000001",
                                ["name"] = "Bac Bo"
                            },
                            ["dealer"] = new JsonObject
                            {
                                ["uid"] = "tts137797_______",
                                ["name"] = "Nicole"
                            },
                            ["currency"] = "PHP",
                            ["participants"] = new JsonArray
                            {
                                new JsonObject {
                                    ["casinoId"] = "babylonpega20001",
                                    ["playerId"] = "4ugutdyww4li",
                                    ["screenName"] = "Chasing the dragon",
                                    ["playerGameId"] = "1776ae69497e716d7b64c0cf-qkqa4qvuijjorqkl",
                                    ["sessionId"] = "qkqa4qvuijjorqklrgtwbymynl4rgad2914b9fd4",
                                    ["casinoSessionId"] = "NEtNRlgLPoPshTZAG9r4qEqbh1rC9uQPb8CNnXSFmjUhRK9lrV8NWuL0mkx1SoZK2",
                                    ["currency"] = "PHP",
                                    ["bets"] = new JsonArray {
                                        new JsonObject {
                                            ["code"] = "BacBo_Player",
                                            ["description"] = "Player",
                                            ["stake"] = 300,
                                            ["payout"] = 270,
                                            ["placedOn"] = "2023-07-30T15:21:31.388Z",
                                            ["transactionId"] = "690274530466494586",
                                            ["currencyRateVersion"] = "rgsbsktabqeaaaam"
                                        },
                                        new JsonObject {
                                            ["code"] = "BacBo_Tie",
                                            ["description"] = "Tie",
                                            ["stake"] = 100,
                                            ["payout"] = 700,
                                            ["placedOn"] = "2023-07-30T15:21:31.388Z",
                                            ["transactionId"] = "690274530466494586",
                                            ["currencyRateVersion"] = "rgsbsktabqeaaaam"
                                        }
                                    },
                                    ["configOverlays"] = new JsonArray(),
                                    ["playMode"] = "RealMoney",
                                    ["channel"] = "mobile",
                                    ["os"] = "Android",
                                    ["device"] = "SmartPhone",
                                    ["skinId"] = "PB",
                                    ["brandId"] = "PB"
                                }
                            },
                            ["result"] = new JsonObject
                            {
                                ["outcome"] = "Tie9or5",
                                ["playerDice"] = new JsonObject {
                                    ["score"] = 9,
                                    ["first"] = 6,
                                    ["second"] = 3
                                },
                                ["bankerDice"] = new JsonObject {
                                    ["score"] = 9,
                                    ["first"] = 6,
                                    ["second"] = 3
                                },
                            },
                            ["wager"] = 400.0m,
                            ["payout"] = 970.0m
                        }
                    }
                }
            }
        };
        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        uriBuilder.Query = query.ToString();
        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, uriBuilder.ToString())
            .WithHeaders("Authorization", $"Basic {authorization}")
            .Respond(HttpStatusCode.OK, new StringContent(response.ToJsonString(), Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var actual = await client
            .GetGameHistoryAsync(
            apiUrl,
            authorization,
            startDate,
            endDate)
            .ToArrayAsync()
            .ConfigureAwait(false)
            ?? Enumerable.Empty<GameHistoryInfo>();

        Assert.NotNull(actual);
        Assert.Equal("1776ae69497e716d7b64c0cf", actual.ElementAt(0).ExternalRoundId);
        Assert.Equal("BacBo00000000001", actual.ElementAt(0).ExternalGameId);
        Assert.Equal("4ugutdyww4li", actual.ElementAt(0).PlayerCode);
        Assert.False(actual.ElementAt(0).IsCancel);
        Assert.False(actual.ElementAt(0).IsVoid);
        Assert.Equal(DateTimeOffset.Parse("2023-07-30T15:21:17.352Z"), actual.ElementAt(0).ExternalCreatedOn);
        Assert.Equal(DateTimeOffset.Parse("2023-07-30T15:21:46.138Z"), actual.ElementAt(0).ExternalClosedOn);
        Assert.Equal(400, actual.ElementAt(0).Bet);
        Assert.Equal(970, actual.ElementAt(0).Settle);
        Assert.Equal(response["data"]!.AsArray().ElementAt(0)!["games"]!.AsArray().ElementAt(0)!["result"]!.ToJsonString(), actual.ElementAt(0).AdditionalData);
    }

    [Fact]
    public async Task Game_is_Roulette()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.Parse("2023-08-03T07:31:00+08:00");
        var endDate = DateTimeOffset.Parse("2023-08-03T07:32:00+08:00");

        var response = new JsonObject
        {
            ["uuid"] = "97789bc4-942b-48c6-bf86-4d16628d8205",
            ["timestamp"] = "2023-08-03T02:24:21.770626793",
            ["data"] = new JsonArray
            {
                new JsonObject {
                    ["date"] = "2023-08-02",
                    ["games"] = new JsonArray
                    {
                        new JsonObject {
                            ["id"] = "1777b4de089cbccca287aae1",
                            ["gameProvider"] = "evolution",
                            ["startedAt"] = "2023-08-02T23:30:50.821Z",
                            ["settledAt"] = "2023-08-02T23:31:26.519Z",
                            ["status"] = "Resolved",
                            ["gameType"] = "roulette",
                            ["table"] = new JsonObject
                            {
                                ["id"] = "vctlz20yfnmp1ylr",
                                ["name"] = "Roulette"
                            },
                            ["dealer"] = new JsonObject
                            {
                                ["uid"] = "tts110871_______",
                                ["name"] = "Viktorija"
                            },
                            ["currency"] = "PHP",
                            ["participants"] = new JsonArray
                            {
                                new JsonObject {
                                    ["casinoId"] = "babylonpega20001",
                                    ["playerId"] = "4uh8yx2hhlkw",
                                    ["screenName"] = "4uh8yx2hhlkw",
                                    ["playerGameId"] = "1777b4de089cbccca287aae1-qkpxsq4fqm7okvf4",
                                    ["sessionId"] = "qkpxsq4fqm7okvf4rg4i7poz2i3lguac03588c76",
                                    ["casinoSessionId"] = "otO6VQq4Fel8RHvwXksIixug9TQkrI9le91NKXjDQZpmDyZMNYQQ2CcmGAv8XiMUi",
                                    ["currency"] = "PHP",
                                    ["bets"] = new JsonArray {
                                        new JsonObject {
                                            ["code"] = "ROU_Black",
                                            ["description"] = "Black",
                                            ["stake"] = 25,
                                            ["payout"] = 50,
                                            ["placedOn"] = "2023-08-02T23:31:11.034Z",
                                            ["transactionId"] = "690542902016012796",
                                            ["currencyRateVersion"] =  "rgzyyptabqeaaaap"
                                        }
                                    },
                                    ["configOverlays"] = new JsonArray(),
                                    ["playMode"] = "RealMoney",
                                    ["channel"] = "mobile",
                                    ["os"] = "iOS",
                                    ["device"] = "SmartPhone",
                                    ["skinId"] = "PB",
                                    ["brandId"] = "PB",
                                    ["betCoverage"] =  new JsonObject {
                                        ["simple"]=0.49m
                                    }
                                }
                            },
                            ["result"] = new JsonObject
                            {
                                ["outcomes"] = new JsonArray
                                {
                                    new JsonObject {
                                        ["number"] = "11",
                                        ["type"] = "Odd",
                                        ["second"] = "Black"
                                    }
                                }
                            },
                            ["wager"] = 25.0m,
                            ["payout"] = 50.0m
                        }
                    }
                }
            }
        };
        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        uriBuilder.Query = query.ToString();
        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, uriBuilder.ToString())
            .WithHeaders("Authorization", $"Basic {authorization}")
            .Respond(HttpStatusCode.OK, new StringContent(response.ToJsonString(), Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var actual = await client
            .GetGameHistoryAsync(
            apiUrl,
            authorization,
            startDate,
            endDate)
            .ToArrayAsync()
            .ConfigureAwait(false)
            ?? Enumerable.Empty<GameHistoryInfo>();

        Assert.NotNull(actual);
        Assert.Equal("1777b4de089cbccca287aae1", actual.ElementAt(0).ExternalRoundId);
        Assert.Equal("vctlz20yfnmp1ylr", actual.ElementAt(0).ExternalGameId);
        Assert.Equal("4uh8yx2hhlkw", actual.ElementAt(0).PlayerCode);
        Assert.False(actual.ElementAt(0).IsCancel);
        Assert.False(actual.ElementAt(0).IsVoid);
        Assert.Equal(DateTimeOffset.Parse("2023-08-02T23:30:50.821Z"), actual.ElementAt(0).ExternalCreatedOn);
        Assert.Equal(DateTimeOffset.Parse("2023-08-02T23:31:26.519Z"), actual.ElementAt(0).ExternalClosedOn);
        Assert.Equal(25, actual.ElementAt(0).Bet);
        Assert.Equal(50, actual.ElementAt(0).Settle);
        Assert.Equal(response["data"]!.AsArray().ElementAt(0)!["games"]!.AsArray().ElementAt(0)!["result"]!.ToJsonString(), actual.ElementAt(0).AdditionalData);
    }

    [Fact]
    public async Task No_Data()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.Parse("2023-07-20T00:58:00+08:00");
        var endDate = DateTimeOffset.Parse("2023-07-20T00:59:00+08:00");

        var response = new JsonObject
        {
            ["uuid"] = "45b16e2c-de48-4b4e-9bbb-5ea3a348a27c",
            ["timestamp"] = "2023-08-02T08:19:44.413633805",
            ["data"] = new JsonArray { }
        };
        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        uriBuilder.Query = query.ToString();
        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, uriBuilder.ToString())
            .WithHeaders("Authorization", $"Basic {authorization}")
            .Respond(HttpStatusCode.OK, new StringContent(response.ToJsonString(), Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var actual = await client
            .GetGameHistoryAsync(
            apiUrl,
            authorization,
            startDate,
            endDate)
            .ToArrayAsync()
            .ConfigureAwait(false)
            ?? Enumerable.Empty<GameHistoryInfo>();

        Assert.NotNull(actual);
        Assert.False(actual.Any());
    }

    [Fact]
    public async Task Player_Currency_Is_Different_From_Table_Currency()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.Parse("2023-07-20T00:58:00+08:00");
        var endDate = DateTimeOffset.Parse("2023-07-20T00:59:00+08:00");

        var response = new JsonObject
        {
            ["uuid"] = "0b232f53-c33d-45f2-9859-2067f12dbe1e",
            ["timestamp"] = "2023-08-02T08:01:33.613770287",
            ["data"] = new JsonArray
            {
              new JsonObject{
                  ["date"] = "2023-07-19",
                  ["games"] = new JsonArray
                  {
                      new JsonObject{
                        ["id"] = "177353504923d2c74daf477b",
                        ["gameProvider"] = "btg",
                        ["startedAt"] = "2023-07-19T16:58:09.483Z",
                        ["settledAt"] = "2023-07-19T16:58:09.780Z",
                        ["status"] = "Resolved",
                        ["gameType"] = "millionaire",
                        ["table"] = new JsonObject
                        {
                            ["id"] = "millionairev5000",
                            ["name"] = "Millionaire"
                        },
                        ["dealer"] = new JsonObject
                        {
                            ["uid"] = "no-dealer",
                            ["name"] = "No Dealer"
                        },
                        ["currency"] = "PHP",
                        ["participants"] = new JsonArray
                        {
                            new JsonObject{
                                ["casinoId"] = "babylonpega20001",
                                ["playerId"] = "4ujxhjitxpgr",
                                ["screenName"] = "4ujxhjitxpgr",
                                ["playerGameId"] = "177353504923d2c74daf477b",
                                ["sessionId"] = "rakidsvaq233k234rfxoxjnk4xahd5rkafa5dea6",
                                ["casinoSessionId"] = "LM6AglEFQhOTErOAgmQpQTANSsM40NsUotQoEtQ2GcMMpsZH3Pl4rmj3DauTYS8SA2",
                                ["currency"] = "HKD",
                                ["bets"] = new JsonArray {
                                    new JsonObject{
                                        ["code"] = "Spin",
                                        ["description"] = "Spin",
                                        ["stake"] = 50,
                                        ["payout"] = 12.5m,
                                        ["placedOn"] = "2023-07-19T16:58:09.501Z",
                                        ["transactionId"] = "689749544069883638",
                                        ["currencyRateVersion"] = "rfvxgnthbqeaaaab"
                                    }
                                },
                                ["configOverlays"] = new JsonArray(),
                                ["playMode"] = "RealMoney",
                                ["mathId"] = "millionaire_base_v5",
                                ["channel"] = "mobile",
                                ["os"] = "Android",
                                ["device"] = "SmartPhone",
                                ["skinId"] = "King8",
                                ["brandId"] = "King8"
                            }
                        },
                        ["result"] = new JsonObject
                        {
                            ["link"] = "/api/render/v1/html/ZHJ8YmFieWxvbnBlZ2EyMDAwMXwxNzczNTM1MDQ5MjNkMmM3NGRhZjQ3N2I"
                        },
                        ["wager"] = 348.030389m,
                        ["payout"] = 87.007597m
                       }
                    }
                }
            }
        };
        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        uriBuilder.Query = query.ToString();
        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, uriBuilder.ToString())
            .WithHeaders("Authorization", $"Basic {authorization}")
            .Respond(HttpStatusCode.OK, new StringContent(response.ToJsonString(), Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var actual = await client
            .GetGameHistoryAsync(
            apiUrl,
            authorization,
            startDate,
            endDate)
            .ToArrayAsync()
            .ConfigureAwait(false)
            ?? Enumerable.Empty<GameHistoryInfo>();

        Assert.NotNull(actual);
        Assert.Equal("177353504923d2c74daf477b", actual.ElementAt(0).ExternalRoundId);
        Assert.Equal("millionairev5000", actual.ElementAt(0).ExternalGameId);
        Assert.Equal("4ujxhjitxpgr", actual.ElementAt(0).PlayerCode);
        Assert.False(actual.ElementAt(0).IsVoid);
        Assert.Equal(DateTimeOffset.Parse("2023-07-19T16:58:09.483Z"), actual.ElementAt(0).ExternalCreatedOn);
        Assert.Equal(DateTimeOffset.Parse("2023-07-19T16:58:09.780Z"), actual.ElementAt(0).ExternalClosedOn);
        Assert.Equal(50, actual.ElementAt(0).Bet);
        Assert.Equal(12.5m, actual.ElementAt(0).Settle);
        Assert.Equal(response["data"]!.AsArray().ElementAt(0)!["games"]!.AsArray().ElementAt(0)!["result"]!.ToJsonString(), actual.ElementAt(0).AdditionalData);
    }

    [Fact]
    public async Task Status_Is_Cancelled()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.Parse("2023-07-29T16:17:00+08:00");
        var endDate = DateTimeOffset.Parse("2023-07-29T16:18:00+08:00");

        var response = new JsonObject
        {
            ["uuid"] = "81618bde-7e9c-465a-9bc4-1f1619b6679c",
            ["timestamp"] = "2023-08-02T07:09:11.258942366",
            ["data"] = new JsonArray
            {
              new JsonObject{
                  ["date"] = "2023-07-29",
                  ["games"] = new JsonArray
                  {
                      new JsonObject{
                        ["id"] = "177648ae4852083a345a4bc6",
                        ["gameProvider"] = "evolution",
                        ["startedAt"] = "2023-07-29T08:17:03.501Z",
                        ["settledAt"] = "2023-07-29T08:17:58.979Z",
                        ["status"] = "Cancelled",
                        ["gameType"] = "baccarat",
                        ["table"] = new JsonObject
                        {
                            ["id"] = "pwsaqk24fcz5qpcr",
                            ["name"] = "Emperor Speed Baccarat C"
                        },
                        ["dealer"] = new JsonObject
                        {
                            ["uid"] = "tts144642_______",
                            ["name"] = "Lea"
                        },
                        ["currency"] = "PHP",
                        ["participants"] = new JsonArray
                        {
                            new JsonObject{
                                ["casinoId"] = "babylonpega20001",
                                ["playerId"] = "4ui0010qu2m0",
                                ["screenName"] = "4ui0010qu2m0",
                                ["playerGameId"] = "177648ae4852083a345a4bc6-qocbhtcgkjvtgluj",
                                ["sessionId"] = "qocbhtcgkjvtglujrgqkuc3f3igkzop3248e6414",
                                ["casinoSessionId"] = "9EOoygvqUtNTR3ZVhfV5l17TOBprgLO6R1zGFUUx4Gwzw0fM7xPXSQ54jFdQgsqpi",
                                ["currency"] = "PHP",
                                ["bets"] = new JsonArray {
                                    new JsonObject{
                                        ["code"] = "BAC_Player",
                                        ["description"] = "Player",
                                        ["stake"] = 50,
                                        ["payout"] = 50,
                                        ["placedOn"] = "2023-07-29T08:17:17.053Z",
                                        ["transactionId"] = "690550180116482128",
                                        ["currencyRateVersion"] = "rgppfttabqeaaaal"
                                    },
                                    new JsonObject{
                                        ["code"]= "BAC_Tie",
                                        ["description"]= "Tie",
                                        ["stake"]= 50,
                                        ["payout"]= 50,
                                        ["placedOn"]= "2023-07-29T08:17:17.053Z",
                                        ["transactionId"]= "690550180116482128",
                                        ["currencyRateVersion"]= "rgppfttabqeaaaal"
                                    }
                                },
                                ["configOverlays"] = new JsonArray(),
                                ["subType"] = "redenvelope",
                                ["playMode"] = "RealMoney",
                                ["channel"] = "mobile",
                                ["os"] = "Android",
                                ["device"] = "SmartPhone",
                                ["skinId"] = "PB",
                                ["brandId"] = "PB"
                            }
                        },
                        ["result"] = new JsonObject
                        {
                            ["banker"] = new JsonObject
                            {
                                ["score"] = 0,
                                ["cards"] = new JsonArray()
                            },
                            ["player"] = new JsonObject
                            {
                                ["score"]=0,
                                ["cards"]= new JsonArray()
                            },
                            ["outcome"] = "None"
                        },
                        ["wager"] = 100.0m,
                        ["payout"] = 100.0m
                       }
                    }
                }
            }
        };
        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        uriBuilder.Query = query.ToString();
        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, uriBuilder.ToString())
            .WithHeaders("Authorization", $"Basic {authorization}")
            .Respond(HttpStatusCode.OK, new StringContent(response.ToJsonString(), Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var actual = await client
            .GetGameHistoryAsync(
            apiUrl,
            authorization,
            startDate,
            endDate)
            .ToArrayAsync()
            .ConfigureAwait(false)
            ?? Enumerable.Empty<GameHistoryInfo>();

        Assert.NotNull(actual);
        Assert.Equal("177648ae4852083a345a4bc6", actual.ElementAt(0).ExternalRoundId);
        Assert.Equal("pwsaqk24fcz5qpcr", actual.ElementAt(0).ExternalGameId);
        Assert.Equal("4ui0010qu2m0", actual.ElementAt(0).PlayerCode);
        Assert.False(actual.ElementAt(0).IsCancel);
        Assert.True(actual.ElementAt(0).IsVoid);
        Assert.Equal(DateTimeOffset.Parse("2023-07-29T08:17:03.501Z"), actual.ElementAt(0).ExternalCreatedOn);
        Assert.Equal(DateTimeOffset.Parse("2023-07-29T08:17:58.979Z"), actual.ElementAt(0).ExternalClosedOn);
        Assert.Equal(100, actual.ElementAt(0).Bet);
        Assert.Equal(100, actual.ElementAt(0).Settle);
        Assert.Equal(response["data"]!.AsArray().ElementAt(0)!["games"]!.AsArray().ElementAt(0)!["result"]!.ToJsonString(), actual.ElementAt(0).AdditionalData);
    }

    [Fact]
    public async Task Status_Is_Resolved_Player_Has_Removed_Their_Bet()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.Parse("2023-07-10T21:22:00+08:00");
        var endDate = DateTimeOffset.Parse("2023-07-10T21:25:00+08:00");

        var response = new JsonObject
        {
            ["uuid"] = "4b46ab2c-cf22-4246-a3e6-3e21266524e5",
            ["timestamp"] = "2023-08-03T00:49:40.061511039",
            ["data"] = new JsonArray
            {
              new JsonObject{
                  ["date"] = "2023-07-10",
                  ["games"] = new JsonArray
                  {
                      new JsonObject{
                        ["id"] = "1770845006be6caf347bdd1f",
                        ["gameProvider"] = "evolution",
                        ["startedAt"] = "2023-07-10T13:22:19.504Z",
                        ["settledAt"] = "2023-07-10T13:24:25.116Z",
                        ["status"] = "Resolved",
                        ["gameType"] = "craps",
                        ["table"] = new JsonObject
                        {
                            ["id"] = "Craps00000000001",
                            ["name"] = "Craps"
                        },
                        ["dealer"] = new JsonObject
                        {
                            ["uid"] = "tts119262_______",
                            ["name"] = "Jurgis"
                        },
                        ["currency"] = "PHP",
                        ["participants"] = new JsonArray
                        {
                            new JsonObject{
                                ["casinoId"] = "babylonpega20001",
                                ["playerId"] = "4ugqv6fk4iqw",
                                ["screenName"] = "4ugqv6fk4iqw",
                                ["sessionId"] = "qkpyquejkicolfq6re77nxsnpfdkwg7697c76cda",
                                ["casinoSessionId"] = "ajAWuBGgA3ZrZEkZMybYdbp9aEARZRi11kHED1k1tIvig2hkqJxOzcEG6oiOqdwXp",
                                ["currency"] = "PHP",
                                ["bets"] = new JsonArray {},
                                ["configOverlays"] = new JsonArray(),
                                ["playMode"] = "RealMoney",
                                ["channel"] = "mobile",
                                ["os"] = "Android",
                                ["device"] = "SmartPhone",
                                ["skinId"] = "PB",
                                ["brandId"] = "PB"
                            }
                        },
                        ["result"] = new JsonObject
                        {
                            ["rolls"] = new JsonArray
                            {
                               new JsonObject{
                                   ["rollId"] = "1770844e79d623ffba71fdf6",
                                   ["startedAt"] = "2023-07-10T13:22:12.850Z",
                                   ["result"] = new JsonObject
                                   {
                                       ["first"] = 1,
                                       ["second"] = 3
                                   }
                               },
                               new JsonObject{
                                   ["rollId"] = "17708456664d43ad13304fb5",
                                   ["startedAt"] = "2023-07-10T13:22:46.882Z",
                                   ["result"] = new JsonObject
                                   {
                                       ["first"] = 2,
                                       ["second"] = 3
                                   }
                               },
                               new JsonObject{
                                   ["rollId"] = "1770845e132b4257b31589f6",
                                   ["startedAt"] = "2023-07-10T13:23:19.847Z",
                                   ["result"] = new JsonObject
                                   {
                                       ["first"] = 2,
                                       ["second"] = 6
                                   }
                               },
                               new JsonObject{
                                   ["rollId"] = "17708466b1bba52b07395de2",
                                   ["startedAt"] = "2023-07-10T13:23:56.867Z",
                                   ["result"] = new JsonObject
                                   {
                                       ["first"] = 6,
                                       ["second"] = 6
                                   }
                               }
                            }
                        },
                        ["wager"] = 0m,
                        ["payout"] = 0m
                       }
                    }
                }
            }
        };
        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        uriBuilder.Query = query.ToString();
        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, uriBuilder.ToString())
            .WithHeaders("Authorization", $"Basic {authorization}")
            .Respond(HttpStatusCode.OK, new StringContent(response.ToJsonString(), Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var actual = await client
            .GetGameHistoryAsync(
            apiUrl,
            authorization,
            startDate,
            endDate)
            .ToArrayAsync()
            .ConfigureAwait(false)
            ?? Enumerable.Empty<GameHistoryInfo>();

        Assert.NotNull(actual);
        Assert.Equal("1770845006be6caf347bdd1f", actual.ElementAt(0).ExternalRoundId);
        Assert.Equal("Craps00000000001", actual.ElementAt(0).ExternalGameId);
        Assert.Equal("4ugqv6fk4iqw", actual.ElementAt(0).PlayerCode);
        Assert.False(actual.ElementAt(0).IsCancel);
        Assert.False(actual.ElementAt(0).IsVoid);
        Assert.Equal(DateTimeOffset.Parse("2023-07-10T13:22:19.504Z"), actual.ElementAt(0).ExternalCreatedOn);
        Assert.Equal(DateTimeOffset.Parse("2023-07-10T13:24:25.116Z"), actual.ElementAt(0).ExternalClosedOn);
        Assert.Equal(0, actual.ElementAt(0).Bet);
        Assert.Equal(0, actual.ElementAt(0).Settle);
        Assert.Equal(response["data"]!.AsArray().ElementAt(0)!["games"]!.AsArray().ElementAt(0)!["result"]!.ToJsonString(), actual.ElementAt(0).AdditionalData);
    }

    [Fact]
    public async Task More_Than_1_participant()
    {
        var apiUrl = new Uri("http://localhost");
        var authorization = "authorization";
        var startDate = DateTimeOffset.Parse("2023-07-10T21:22:00+08:00");
        var endDate = DateTimeOffset.Parse("2023-07-10T21:25:00+08:00");

        var response = new JsonObject
        {
            ["uuid"] = "40d80f2f-2f1c-43f9-9e49-b9395883ff7a",
            ["timestamp"] = "2023-08-03T07:57:39.840545834",
            ["data"] = new JsonArray
            {
              new JsonObject{
                  ["date"] = "2023-07-31",
                  ["games"] = new JsonArray
                  {
                      new JsonObject{
                        ["id"] = "1776f9341cfa969d4b939e4a",
                        ["gameProvider"] = "evolution",
                        ["startedAt"] = "2023-07-31T14:11:52.344Z",
                        ["settledAt"] = "2023-07-31T14:14:16.320Z",
                        ["status"] = "Cancelled",
                        ["gameType"] = "megaball",
                        ["table"] = new JsonObject
                        {
                            ["id"] = "MegaBall00000001",
                            ["name"] = "Mega Ball"
                        },
                        ["dealer"] = new JsonObject
                        {
                            ["uid"] = "tts125999_______",
                            ["name"] = "Arianna"
                        },
                        ["currency"] = "PHP",
                        ["participants"] = new JsonArray
                        {
                            new JsonObject{
                                ["casinoId"] = "babylonpega20001",
                                ["playerId"] = "4uk0lg559sut",
                                ["screenName"] = "4uk0lg559sut",
                                ["playerGameId"] = "1776f9341cfa969d4b939e4a-ress7t7qlwpgwyrq",
                                ["sessionId"] = "ress7t7qlwpgwyrqrgwdcd5jnl4xlkscabfedea0",
                                ["casinoSessionId"] = "rYFXkCnOodkat2McoHbQSzyxWt3bLbBZrvG0qERTOg6qbss02XXGNcrgRTGgh3gym",
                                ["currency"] = "PHP",
                                ["bets"] = new JsonArray {
                                    new JsonObject{
                                        ["code"]= "MB_6Cards",
									["description"]= "6 Mega Ball Cards",
									["stake"]= 30,
									["payout"]=30,
									["placedOn"]= "2023-07-31T14:12:05.369Z",
									["transactionId"]= "690806269826092102",
									["currencyRateVersion"]= "rgut7btabqeaaaan"
                                    }
                                },
                                ["configOverlays"] = new JsonArray(),
                                ["playMode"] = "RealMoney",
                                ["channel"] = "mobile",
                                ["os"] = "Android",
                                ["device"] = "SmartPhone",
                                ["skinId"] = "PB",
                                ["brandId"] = "PB"
                            },
                            new JsonObject{
                                ["casinoId"] = "babylonpega20001",
                                ["playerId"] = "4uki9crgioxc",
                                ["screenName"] = "4uki9crgioxc",
                                ["playerGameId"] = "1776f9341cfa969d4b939e4a-rfy42hpdtnzjvzc4",
                                ["sessionId"] = "rfy42hpdtnzjvzc4rgweh3wwsfwrfvxj36f93dca",
                                ["casinoSessionId"] = "XvQMJEivJt7P35CMf2T2cdFhIhJqwQvZQ6mpeQrNnQokOH1OsG9WEbvHKDye4bGuo",
                                ["currency"] = "PHP",
                                ["bets"] = new JsonArray {
                                    new JsonObject{
                                        ["code"]= "MB_3Cards",
                                    ["description"]= "3 Mega Ball Cards",
                                    ["stake"]= 15,
                                    ["payout"]=15,
                                    ["placedOn"]= "2023-07-31T14:12:05.368Z",
                                    ["transactionId"]=  "690806269825997102",
                                    ["currencyRateVersion"]= "rgut7btabqeaaaan"
                                    }
                                },
                                ["configOverlays"] = new JsonArray(),
                                ["playMode"] = "RealMoney",
                                ["channel"] = "mobile",
                                ["os"] = "Android",
                                ["device"] = "SmartPhone",
                                ["skinId"] = "PB",
                                ["brandId"] = "PB"
                            }
                        },
                        ["result"] = new JsonObject
                        {
                            ["drawnBalls"] = new JsonArray(),
                            ["drawnBalls"] = new JsonArray
                            {
                                new JsonObject
                                {
                                    ["multiplier"]=5
                                }
                            }
                        },
                        ["wager"] =45.0m,
                        ["payout"] = 45.0m
                       }
                    }
                }
            }
        };
        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        uriBuilder.Query = query.ToString();
        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, uriBuilder.ToString())
            .WithHeaders("Authorization", $"Basic {authorization}")
            .Respond(HttpStatusCode.OK, new StringContent(response.ToJsonString(), Encoding.UTF8, "application/json"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var actual = await client
            .GetGameHistoryAsync(
            apiUrl,
            authorization,
            startDate,
            endDate)
            .ToArrayAsync()
            .ConfigureAwait(false)
            ?? Enumerable.Empty<GameHistoryInfo>();

        Assert.NotNull(actual);
        Assert.Equal("1776f9341cfa969d4b939e4a", actual.ElementAt(0).ExternalRoundId);
        Assert.Equal("MegaBall00000001", actual.ElementAt(0).ExternalGameId);
        Assert.Equal("4uk0lg559sut", actual.ElementAt(0).PlayerCode);
        Assert.False(actual.ElementAt(0).IsCancel);
        Assert.True(actual.ElementAt(0).IsVoid);
        Assert.Equal(DateTimeOffset.Parse("2023-07-31T14:11:52.344Z"), actual.ElementAt(0).ExternalCreatedOn);
        Assert.Equal(DateTimeOffset.Parse("2023-07-31T14:14:16.320Z"), actual.ElementAt(0).ExternalClosedOn);
        Assert.Equal(30, actual.ElementAt(0).Bet);
        Assert.Equal(30, actual.ElementAt(0).Settle);
        Assert.Equal(response["data"]!.AsArray().ElementAt(0)!["games"]!.AsArray().ElementAt(0)!["result"]!.ToJsonString(), actual.ElementAt(0).AdditionalData);



        Assert.Equal("1776f9341cfa969d4b939e4a", actual.ElementAt(1).ExternalRoundId);
        Assert.Equal("MegaBall00000001", actual.ElementAt(1).ExternalGameId);
        Assert.Equal("4uki9crgioxc", actual.ElementAt(1).PlayerCode);
        Assert.False(actual.ElementAt(1).IsCancel);
        Assert.True(actual.ElementAt(1).IsVoid);
        Assert.Equal(15, actual.ElementAt(1).Bet);
        Assert.Equal(15, actual.ElementAt(1).Settle);
        Assert.Equal(response["data"]!.AsArray().ElementAt(0)!["games"]!.AsArray().ElementAt(0)!["result"]!.ToJsonString(), actual.ElementAt(1).AdditionalData);
    }
}
