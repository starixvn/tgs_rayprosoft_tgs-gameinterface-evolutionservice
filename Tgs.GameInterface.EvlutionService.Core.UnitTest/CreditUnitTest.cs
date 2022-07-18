using System.Globalization;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RichardSzalay.MockHttp;
using Tgs.Logging;

namespace Tgs.GameInterface.EvolutionService.Core.UnitTest;

[TestClass]
public class CreditUnitTest
{
    [TestMethod]
    public async Task Can_Credit()
    {
        var apiUrl = new Uri("http://localhost");
        var cCode = "ECR";
        var ecId = "ecId";
        var playerCode = "4ucmqplxb029";
        var amount = 100;
        var transactionsId = "transactionsId";

        var response = @"
                <transfer>
                <result>Y</result>
                <balance>25.1354</balance >
                <etransid>123abc</etransid>
                <transid>456def</transid>
                <datetime>Mon Feb 5 11:11:11 GMT 2018</datetime>
                <euid>4ucmqplxb029</euid>
                <uid>user123</uid>
                </transfer>";

        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Post, "/api/ecashier")
            .WithQueryString("cCode", cCode)
            .WithQueryString("ecID", ecId)
            .WithQueryString("euID", playerCode)
            .WithQueryString("amount", amount.ToString())
            .WithQueryString("eTransID", transactionsId)
            .WithQueryString("createuser", "Y")
            .WithQueryString("output", "1")
            .Respond(HttpStatusCode.OK, new StringContent(response, Encoding.UTF8, "application/xml"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var fakeTgsLog = Substitute.For<ITgsLog>();
        var client = new GameApiClient(fakeHttpClient, fakeLog, fakeTgsLog);

        var result = await client.CreditAsync(apiUrl, cCode, ecId, playerCode, 100, transactionsId, "Y", default);

        Assert.AreEqual("Y", result.Result);
        Assert.AreEqual(playerCode, result.EuId);
    }
}