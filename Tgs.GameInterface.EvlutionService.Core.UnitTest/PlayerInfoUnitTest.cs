using System.Net;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RichardSzalay.MockHttp;
using Tgs.Logging;

namespace Tgs.GameInterface.EvolutionService.Core.UnitTest;

[TestClass]
public class PlayerInfoUnitTest
{
    [TestMethod]
    public async Task Can_Get_Player_Info()
    {
        var apiUrl = new Uri("http://localhost");
        var cCode = "GUI";
        var ecId = "ecId";
        var playerCode = "4ucmqplxb029";

        var response = @"
                <userdetails>
                <emailaddress>john@company.com</emailaddress>
                <firstname>John</firstname>
                <lastname>Doe</lastname>
                <screenname>evolution</screenname>
                <countrycode>UK</countrycode>
                <uid>user123</uid>
                <euid>4ucmqplxb029</euid>
                </userdetails>";

        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, "/api/ecashier")
            .WithQueryString("cCode", cCode)
            .WithQueryString("ecID", ecId)
            .WithQueryString("euID", playerCode)
            .WithQueryString("output", "1")
            .Respond(HttpStatusCode.OK, new StringContent(response, Encoding.UTF8, "application/xml"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var fakeTgsLog = Substitute.For<ITgsLog>();
        var client = new GameApiClient(fakeHttpClient, fakeLog, fakeTgsLog);

        var result = await client.GetPlayerInfoAsync(apiUrl, cCode, ecId, playerCode, default);

        Assert.AreEqual(playerCode, result.EuId);
    }
}