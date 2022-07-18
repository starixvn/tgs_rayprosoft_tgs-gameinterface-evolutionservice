using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using Tgs.GameInterface.EvolutionService.Core.Models;
using Tgs.Logging;

namespace Tgs.GameInterface.EvolutionService.Core;

public class GameApiClient : IGameApiClient
{
    private static readonly JsonSerializerOptions? SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    private readonly HttpClient _client;
    private readonly ILogger<GameApiClient> _logger;
    private readonly ITgsLog _tgsLog;

    public GameApiClient(
        HttpClient client,
        ILogger<GameApiClient> logger,
        ITgsLog tgsLog
    )
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tgsLog = tgsLog ?? throw new ArgumentNullException(nameof(tgsLog));
    }

    public async ValueTask<GameUrlResponse> GetGameUrlAsync(Uri apiUrl, GameUrlRequest request,
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();

        var content = new StringContent(JsonSerializer.Serialize(request, SerializerOptions), Encoding.UTF8,
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

    public async ValueTask<BalanceResponse> GetBalanceAsync(Uri apiUrl, string cCode, string ecId, string euId,
        CancellationToken cancellationToken)
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

    public async ValueTask<CreditResponse> CreditAsync(Uri apiUrl, string cCode, string ecId, string euId,
        decimal amount, string eTransId, string createUser, CancellationToken cancellationToken)
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

    public async ValueTask<DebitResponse> DebitAsync(Uri apiUrl, string cCode, string ecId, string euId, decimal amount,
        string eTransId, CancellationToken cancellationToken)
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

    public async ValueTask<PlayerInfoResponse> GetPlayerInfoAsync(Uri apiUrl, string cCode, string ecId, string euId,
        CancellationToken cancellationToken)
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

    public async ValueTask<TransactionInfoResponse> GetTransactionInfoAsync(Uri apiUrl, string cCode, string ecId,
        string euId, string eTransId,
        CancellationToken cancellationToken)
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

    public async ValueTask<bool> KickPlayerAsync(Uri apiUrl, string casinoId, string apiToken, string uuid, string playerCode,
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();

        var content = new
        {
            uuid,
            playerLogin = playerCode
        };

        var response =
            await _client.PostAsync($"{apiUrl}api/external/kickPlayer/v1/{casinoId}/{apiToken}",
                new StringContent(JsonSerializer.Serialize(content, SerializerOptions), Encoding.UTF8, "application/json"), cancellationToken);

        sw.Stop();

        await LogAsync("kickPlayer", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        return response.IsSuccessStatusCode;
    }

    public async ValueTask<string> GetGameHistoryAsync(Uri apiUrl, string authorization, DateTimeOffset? startDate, DateTimeOffset? endDate,
        string? gameType,
        string? channel, string? gameProvider, string? transactionId, string? owTransactionId, bool? rounding,
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();

        var path = $"{apiUrl}api/gamehistory/v1/casino/games?";

        if (startDate is not null)
        {
            path += $"&startDate={startDate.Value.ToUniversalTime():yyyy-MM-ddThh:mm:ss.fffZ}";
        }

        if (endDate is not null)
        {
            path += $"&endDate={endDate.Value.ToUniversalTime():yyyy-MM-ddThh:mm:ss.fffZ}";
        }

        if (!string.IsNullOrEmpty(gameType))
        {
            path += $"&gameType={gameType}";
        }

        if (!string.IsNullOrEmpty(channel))
        {
            path += $"&channel={channel}";
        }

        if (!string.IsNullOrEmpty(gameProvider))
        {
            path += $"&gameProvider={gameProvider}";
        }

        if (!string.IsNullOrEmpty(transactionId))
        {
            path += $"&transactionId={transactionId}";
        }

        if (!string.IsNullOrEmpty(owTransactionId))
        {
            path += $"&owTransactionId={owTransactionId}";
        }

        if (rounding is not null)
        {
            path += $"&rounding={rounding}";
        }

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, path);

        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", authorization);

        var response = await _client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        sw.Stop();

        await LogAsync("getGameHistory", null, response, sw.ElapsedMilliseconds, cancellationToken);

        _ = response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    private async ValueTask LogAsync(string logPrefix, HttpContent? content, HttpResponseMessage httpResponseMessage,
        long time, CancellationToken cancellationToken = default)
    {
        logPrefix = $"{nameof(GameApiClient).ToLower()}.{logPrefix}";

        var responseMessage = default(string);

        using (var sr = new StreamReader(httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken).Result))
        {
            responseMessage = await sr.ReadToEndAsync().ConfigureAwait(false);
        }

        var apiLogEvent = new ApiLogEvent
        {
            Path = httpResponseMessage.RequestMessage?.RequestUri?.AbsoluteUri.NoLongerThan(512) ?? string.Empty,
            Method = httpResponseMessage.RequestMessage?.Method.ToString().NoLongerThan(8) ?? string.Empty,
            Prefix = logPrefix,
            ServerName = Environment.MachineName.NoLongerThan(128),
            RequestHeaders = httpResponseMessage.RequestMessage?.Headers +
                httpResponseMessage.RequestMessage?.Content?.Headers?.ToString(),
            RequestContent =
                content == null
                    ? ""
                    : (await content!.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)).NoLongerThan(4096),
            ResponseHeaders = httpResponseMessage.Headers.ToString(),
            ResponseContent = responseMessage.NoLongerThan(4096),
            ResponseCode = (short)httpResponseMessage.StatusCode,
            ExecutionTimeMs = (int)time,
            RecordedOn = DateTimeOffset.UtcNow
        };
        _tgsLog.LogApi(apiLogEvent);
        _logger.LogInformation("{apiLogEvent}", JsonSerializer.Serialize(apiLogEvent));
    }
}