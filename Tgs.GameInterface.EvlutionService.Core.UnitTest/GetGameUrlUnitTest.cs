using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.EvolutionService.Core.Models;
using Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;
using Tgs.Logging;

namespace Tgs.GameInterface.EvolutionService.Core.UnitTest
{
    [TestClass]
    public class GetGameUrlUnitTest
    {
        [TestMethod]
        public async Task Can_Get_Game_Url()
        {
            var apiUrl = new Uri("http://localhost/ua/v1/key/token");

            var request = new GameUrlRequest
            {
                Uuid = Guid.NewGuid().ToString(),
                Player = new GameUrlPlayer
                {
                    Id = "4ucmqplxb029",
                    Update = false,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Nickname = "4ucmqplxb029",
                    Country = "CN",
                    Language = "cn",
                    Currency = "CNY",
                    Session = new GameUrlPlayerSession
                    {
                        Id = "rDLIWMPV7dgRstCYBzSY6EidJQh51TWgg8b7XgefrRt5QkjiCB5pwwNvRKSdGDPsc",
                        Ip = "127.0.0.1"
                    },
                    Group = default
                },
                Config = new GameUrlConfig
                {
                    Game = new GameUrlConfigGame
                    {
                        Category = "baccarat_sicbo",
                        Table = null
                    },
                    Channel = new GameUrlConfigChannel
                    {
                        Wrapped = true,
                        Mobile = false
                    },
                    Brand = new GameUrlConfigBrand
                    {
                        Skin = "brandCode"
                    }
                }
            };

            var response = new GameUrlResponse
            {
                Entry = "Entry",
                EntryEmbedded = "EntryEmbedded"
            };

            var handler = new MockHttpMessageHandler();

            _ = handler.Expect(HttpMethod.Post, $"/ua/v1/key/token")
                .Respond(HttpStatusCode.OK, new StringContent(JsonSerializer.Serialize(response), Encoding.UTF8, "application/json"));

            var fakeHttpClient = handler.ToHttpClient();
            var fakeLog = NullLogger<GameApiClient>.Instance;
            var fakeTgsLog = Substitute.For<ITgsLog>();
            var client = new GameApiClient(fakeHttpClient, fakeLog, fakeTgsLog);

            var result = await client.GetGameUrlAsync(apiUrl, request, default);

            Assert.AreEqual("Entry", result.Entry);
            Assert.AreEqual("EntryEmbedded", result.EntryEmbedded);
        }
    }
}
