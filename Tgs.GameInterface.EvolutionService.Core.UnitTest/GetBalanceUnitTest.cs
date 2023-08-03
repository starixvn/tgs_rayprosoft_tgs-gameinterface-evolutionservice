using System.Net;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using RichardSzalay.MockHttp;

namespace Tgs.GameInterface.EvolutionService.Core.UnitTest;

public class GetBalanceUnitTest
{
    [Fact]
    public async Task Can_Get_Balance()
    {
        var apiUrl = new Uri("http://localhost");
        var cCode = "RWA";
        var ecId = "ecId";
        var playerCode = "4ucmqplxb029";

        var response = @"
                <userbalance>
                <result>Y</result>
                <euid>extUser123</euid>
                <uid>user123</uid>
                <tbalance>25.1354</tbalance>
                <abalance>25.1354</abalance>
                </userbalance>";

        var handler = new MockHttpMessageHandler();
        _ = handler.Expect(HttpMethod.Get, $"/api/ecashier")
            .WithQueryString("cCode", cCode)
            .WithQueryString("ecID", ecId)
            .WithQueryString("euID", playerCode)
            .WithQueryString("euID", playerCode)
            .WithQueryString("output", "1")
            .Respond(HttpStatusCode.OK, new StringContent(response, Encoding.UTF8, "application/xml"));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeHttpClient, fakeLog);

        var result = await client.GetBalanceAsync(apiUrl, cCode, ecId, playerCode, default);

        Assert.Equal("Y", result.Result);
        Assert.Equal("extUser123", result.PlayerCode);
        Assert.Equal(25.1354M, result.ABalance);
    }
}
