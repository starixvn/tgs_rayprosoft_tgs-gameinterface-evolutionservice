using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Web;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using Tgs.GameInterface.EvolutionService.Core.ApplicationServices.GamHistory;
using Tgs.GameInterface.EvolutionService.Core.Models;

namespace Tgs.GameInterface.EvolutionService.Core;

public class GameApiClient : IGameApiClient
{
    private static readonly JsonSerializerOptions? _SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    private readonly HttpClient _client;
    private readonly ILogger<GameApiClient> _logger;

    public GameApiClient(
        HttpClient client,
        ILogger<GameApiClient> logger
    )
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<CreditResponse> CreditAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        decimal amount,
        string eTransId,
        string createUser,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();

        var response =
            await _client.PostAsync(
                $"{apiUrl}api/ecashier?cCode={cCode}&ecID={ecId}&euID={euId}&amount={amount}&eTransID={eTransId}&createuser={createUser}&output=1",
                new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded"), cancellationToken);

        sw.Stop();

        await LogAsync("credit", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        var serializer = new XmlSerializer(typeof(CreditResponse));

        var memStream =
            new MemoryStream(Encoding.UTF8.GetBytes(await response.Content.ReadAsStringAsync(cancellationToken)));

        return (CreditResponse)serializer.Deserialize(memStream)!;
    }

    public async ValueTask<DebitResponse> DebitAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        decimal amount,
        string eTransId,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();
        var response =
            await _client.PostAsync(
                $"{apiUrl}api/ecashier?cCode={cCode}&ecID={ecId}&euID={euId}&amount={amount}&eTransID={eTransId}&output=1",
                new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded"), cancellationToken);

        sw.Stop();

        await LogAsync("debit", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        var serializer = new XmlSerializer(typeof(DebitResponse));

        var memStream =
            new MemoryStream(Encoding.UTF8.GetBytes(await response.Content.ReadAsStringAsync(cancellationToken)));

        return (DebitResponse)serializer.Deserialize(memStream)!;
    }

    public async ValueTask<BalanceResponse> GetBalanceAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();

        var response =
            await _client.GetAsync($"{apiUrl}api/ecashier?cCode={cCode}&ecID={ecId}&euID={euId}&output=1",
                cancellationToken);

        sw.Stop();

        await LogAsync("getBalance", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        var serializer = new XmlSerializer(typeof(BalanceResponse));

        var memStream =
            new MemoryStream(Encoding.UTF8.GetBytes(await response.Content.ReadAsStringAsync(cancellationToken)));

        return (BalanceResponse)serializer.Deserialize(memStream)!;
    }

    public async ValueTask<string> GetGameHistoryAsync(
        Uri apiUrl,
        string authorization,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        string? gameType,
        string? channel,
        string? gameProvider,
        string? transactionId,
        string? owTransactionId,
        bool? rounding,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();

        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (startDate is not null)
        {
            query["startDate"] = startDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        if (endDate is not null)
        {
            query["endDate"] = endDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        if (!string.IsNullOrEmpty(gameType))
        {
            query["gameType"] = gameType;
        }

        if (!string.IsNullOrEmpty(channel))
        {
            query["channel"] = channel;
        }

        if (!string.IsNullOrEmpty(gameProvider))
        {
            query["gameProvider"] = gameProvider;
        }

        if (!string.IsNullOrEmpty(transactionId))
        {
            query["transactionId"] = transactionId;
        }

        if (!string.IsNullOrEmpty(owTransactionId))
        {
            query["owTransactionId"] = owTransactionId;
        }

        if (rounding is not null)
        {
            query["rounding"] = rounding.ToString();
        }
        uriBuilder.Query = query.ToString();

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", authorization);

        var response = await _client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        sw.Stop();

        await LogAsync("getGameHistory", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async IAsyncEnumerable<GameHistoryInfo> GetGameHistoryAsync(
        Uri apiUrl,
        string authorization,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();
        var uriBuilder = new UriBuilder(new Uri(apiUrl, "/api/gamehistory/v1/casino/games"));
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["startDate"] = startDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        query["endDate"] = endDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        uriBuilder.Query = query.ToString();

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", authorization);

        var httpResponseMessage = await _client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        sw.Stop();

        await LogAsync("getGameHistory", null, httpResponseMessage, sw.ElapsedMilliseconds, cancellationToken);

        _ = httpResponseMessage.EnsureSuccessStatusCode();

        var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

        var jsonNode = JsonNode.Parse(responseMessage)!;
        var data = jsonNode["data"]!.AsArray();
        if (!data.Any())
        {
            yield break;
        }
        var games = data.ElementAt(0)!["games"]!.AsArray();

        if (games is null)
        {
            yield break;
        }

        foreach (var game in games)
        {
            if (game is null)
            {
                continue;
            }
            var gameHistoryRound = game.Deserialize<GamHistoryRoundData>(_SerializerOptions)!;

            var playerCode = gameHistoryRound.Participants.ElementAt(0).PlayerId;
            var externalRoundId = gameHistoryRound.Id;
            var externalGameId = gameHistoryRound.Table.Id;

            var externalCreatedOn = gameHistoryRound.StartedAt;
            var externalClosedOn = gameHistoryRound.SettledAt;
            var bet = gameHistoryRound.Participants.Select(participant => participant.Bets.Sum(bet => bet.Stake)).Sum();
            var settle = gameHistoryRound.Participants.Select(participant => participant.Bets.Sum(bet => bet.Payout)).Sum();
            var additionalData = game.ToJsonString();
            var isVoid = gameHistoryRound.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase);
            var isCancel = bet == 0 && settle == 0;
            yield return new GameHistoryInfo(
                ExternalRoundId: externalRoundId,
                ExternalGameId: externalGameId,
                PlayerCode: playerCode,
                IsCancel: isCancel,
                IsVoid: isVoid,
                ExternalCreatedOn: externalCreatedOn,
                ExternalClosedOn: externalClosedOn,
                Bet: bet,
                Settle: settle,
                AdditionalData: additionalData);
        }
    }

    public async ValueTask<GameUrlResponse> GetGameUrlAsync(
        Uri apiUrl,
        GameUrlRequest request,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();

        var content = new StringContent(JsonSerializer.Serialize(request, _SerializerOptions), Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync(apiUrl, content, cancellationToken);

        sw.Stop();

        await LogAsync("getAuthentication", content, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<GameUrlResponse>(
            await response.Content.ReadAsStringAsync(cancellationToken),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async ValueTask<PlayerInfoResponse> GetPlayerInfoAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();

        var response =
            await _client.GetAsync($"{apiUrl}api/ecashier?cCode={cCode}&ecID={ecId}&euID={euId}&output=1",
                cancellationToken);

        sw.Stop();

        await LogAsync("getPlayerInfo", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        var serializer = new XmlSerializer(typeof(PlayerInfoResponse));

        var memStream =
            new MemoryStream(Encoding.UTF8.GetBytes(await response.Content.ReadAsStringAsync(cancellationToken)));

        return (PlayerInfoResponse)serializer.Deserialize(memStream)!;
    }

    public async ValueTask<TransactionInfoResponse> GetTransactionInfoAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        string eTransId,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();

        var response =
            await _client.GetAsync(
                $"{apiUrl}api/ecashier?cCode={cCode}&ecID={ecId}&euID={euId}&output=1&eTransID={eTransId}",
                cancellationToken);

        sw.Stop();

        await LogAsync("getTransactionInfo", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        var serializer = new XmlSerializer(typeof(TransactionInfoResponse));

        var memStream =
            new MemoryStream(Encoding.UTF8.GetBytes(await response.Content.ReadAsStringAsync(cancellationToken)));

        return (TransactionInfoResponse)serializer.Deserialize(memStream)!;
    }

    public async ValueTask<bool> KickPlayerAsync(
        Uri apiUrl,
        string casinoId,
        string apiToken,
        string uuid,
        string playerCode,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();

        var content = new
        {
            uuid,
            playerLogin = playerCode
        };

        var response =
            await _client.PostAsync($"{apiUrl}api/external/kickPlayer/v1/{casinoId}/{apiToken}",
                new StringContent(JsonSerializer.Serialize(content, _SerializerOptions), Encoding.UTF8, "application/json"), cancellationToken);

        sw.Stop();

        await LogAsync("kickPlayer", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        return response.IsSuccessStatusCode;
    }

    private async ValueTask LogAsync(
        string logPrefix,
        HttpContent? content,
        HttpResponseMessage httpResponseMessage,
        long time,
        CancellationToken cancellationToken = default)
    {
        logPrefix = $"{nameof(GameApiClient).ToLower()}.{logPrefix}";

        var responseMessage = default(string);

        using (var stream = new StreamReader(httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken).Result))
        {
            responseMessage = await stream
                .ReadToEndAsync(
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        var apiLogEvent = new GameApiLogEvent
        {
            Path = httpResponseMessage.RequestMessage?.RequestUri?.AbsoluteUri.NoLongerThan(512) ?? string.Empty,
            Method = httpResponseMessage.RequestMessage?.Method.ToString().NoLongerThan(8) ?? string.Empty,
            Prefix = logPrefix,
            ServerName = Environment.MachineName.NoLongerThan(128),
            RequestHeaders = httpResponseMessage.RequestMessage?.Headers +
                httpResponseMessage.RequestMessage?.Content?.Headers?.ToString(),
            RequestContent =
                content == null
                    ? string.Empty
                    : (await content!.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)).NoLongerThan(4096),
            ResponseHeaders = httpResponseMessage.Headers.ToString(),
            ResponseContent = responseMessage.NoLongerThan(int.MaxValue),
            ResponseCode = (short)httpResponseMessage.StatusCode,
            ExecutionTimeMs = (int)time,
            RecordedOn = DateTimeOffset.UtcNow
        };

        _logger.LogInformation("{apiLogEvent}", JsonSerializer.Serialize(apiLogEvent));
    }
}
