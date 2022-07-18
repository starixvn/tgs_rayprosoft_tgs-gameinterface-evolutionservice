using System.Net;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RichardSzalay.MockHttp;
using Tgs.Logging;

namespace Tgs.GameInterface.EvolutionService.Core.UnitTest;

[TestClass]
public class TransactionInfoUnitTest
{
    [TestMethod]
    public async Task Can_Get_Transaction_Info()
    {
        var apiUrl = new Uri("http://localhost");
        var cCode = "GUI";
        var ecId = "ecId";
        var playerCode = "4ucmqplxb029";
        var transactionsId = "transactionsId";

        var response = @"
                <transaction>
                <result>Y</result>
                <balance>1483.3354</balance>
                <etransid>licenseeTransaction345</etransid>
                <transid>evolutionTransaction123</transid>
                <datetime>Mon Feb 26 11:10:12 GMT 2018</datetime>
                <datetime-iso>2018-02-26T11:10:12.123Z</datetime-iso>
                <uID>user123</uID>
                <euID>4ucmqplxb029</euID>
                <cCode>ECR</cCode>
                <amount>133.1354</amount>
                </transaction>";

        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, "/api/ecashier")
            .WithQueryString("cCode", cCode)
            .WithQueryString("ecID", ecId)
            .WithQueryString("euID", playerCode)
            .WithQueryString("eTransID", transactionsId)
            .WithQueryString("output", "1")
            .Respond(HttpStatusCode.OK, new StringContent(response, Encoding.UTF8, "application/xml"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var fakeTgsLog = Substitute.For<ITgsLog>();
        var client = new GameApiClient(fakeHttpClient, fakeLog, fakeTgsLog);

        var result = await client.GetTransactionInfoAsync(apiUrl, cCode, ecId, playerCode, transactionsId, default);

        Assert.AreEqual(playerCode, result.EuId);
    }
}