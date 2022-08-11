using Google.Protobuf.WellKnownTypes;
using Tgs.GameInterface.EvolutionService.Protos;

namespace Tgs.GameInterface.EvolutionService.Client;

public class GameInterfaceEvolutionClient : IGameInterfaceEvolutionClient
{
    private readonly GameInterfaceEvoService.GameInterfaceEvoServiceClient _client;

    public GameInterfaceEvolutionClient(GameInterfaceEvoService.GameInterfaceEvoServiceClient client)
        => _client = client;

    public async ValueTask<FundInReply> FundInAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        string transactionId,
        decimal amount, CancellationToken cancellationToken = default)
        => await _client.FundInAsync(new()
        {
            ApiUrl = apiUrl.ToString(),
            AgentId = agentId,
            PlayerCode = playerCode,
            TransactionId = transactionId,
            Amount = amount
        }, cancellationToken: cancellationToken);

    public async ValueTask<FundOutReply> FundOutAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        string transactionId, decimal amount,
        CancellationToken cancellationToken = default)
        => await _client.FundOutAsync(new()
        {
            ApiUrl = apiUrl.ToString(),
            AgentId = agentId,
            PlayerCode = playerCode,
            TransactionId = transactionId,
            Amount = amount
        }, cancellationToken: cancellationToken);

    public async ValueTask<GetBalanceReply> GetBalanceAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        CancellationToken cancellationToken = default)
        => await _client.GetBalanceAsync(new()
        {
            ApiUrl = apiUrl.ToString(),
            AgentId = agentId,
            PlayerCode = playerCode
        }, cancellationToken: cancellationToken);

    public async ValueTask<GetGameHistoriesReply> GetGameHistoriesAsync(
        Uri apiUrl,
        string casinoId,
        string gameHistoryApiToken,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        CancellationToken cancellationToken = default)
        => await _client.GetGameHistoriesAsync(new()
        {
            ApiUrl = apiUrl.ToString(),
            CasinoId = casinoId,
            GameHistoryApiToken = gameHistoryApiToken,
            StartDate = startDate.ToTimestamp(),
            EndDate = endDate.ToTimestamp()
        }, cancellationToken: cancellationToken);

    public async ValueTask<GameUrlReply> GetLiveDealerGameUrlAsync(
                        Uri ua2AuthenticationUrl,
        string brandCode,
        string playerCode,
        short platformType,
        string ipAddress,
        string encryptedToken,
        string externalCultureCode,
        string externalCurrencyCode,
        CancellationToken cancellationToken = default)
        => await _client.GetLiveDealerGameUrlAsync(new()
        {
            Ua2AuthenticationUrl = ua2AuthenticationUrl.ToString(),
            BrandCode = brandCode,
            PlayerCode = playerCode,
            PlatformType = platformType,
            IpAddress = ipAddress,
            EncryptedToken = encryptedToken,
            ExternalCultureCode = externalCultureCode,
            ExternalCurrencyCode = externalCurrencyCode
        }, cancellationToken: cancellationToken);

    public async ValueTask<GetPlayerInfoReply> GetPlayerInfoAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        CancellationToken cancellationToken = default)
        => await _client.GetPlayerInfoAsync(new()
        {
            ApiUrl = apiUrl.ToString(),
            AgentId = agentId,
            PlayerCode = playerCode
        }, cancellationToken: cancellationToken);

    public async ValueTask<GameUrlReply> GetSlotGameUrlAsync(
            Uri ua2AuthenticationUrl,
        string brandCode,
        string playerCode,
        short platformType,
        string ipAddress,
        string encryptedToken,
        string externalCultureCode,
        string externalCurrencyCode,
        string externalGameId,
        CancellationToken cancellationToken = default)
        => await _client.GetSlotGameUrlAsync(new()
        {
            Ua2AuthenticationUrl = ua2AuthenticationUrl.ToString(),
            BrandCode = brandCode,
            PlayerCode = playerCode,
            PlatformType = platformType,
            IpAddress = ipAddress,
            EncryptedToken = encryptedToken,
            ExternalCultureCode = externalCultureCode,
            ExternalCurrencyCode = externalCurrencyCode,
            ExternalGameId = externalGameId
        }, cancellationToken: cancellationToken);

    public async ValueTask<GetTransactionInfoReply> GetTransactionInfoAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        string transactionId,
        CancellationToken cancellationToken = default)
        => await _client.GetTransactionInfoAsync(new()
        {
            ApiUrl = apiUrl.ToString(),
            AgentId = agentId,
            PlayerCode = playerCode,
            TransactionId = transactionId
        }, cancellationToken: cancellationToken);

    public async ValueTask<KickPlayerReply> KickPlayerAsync(
        Uri apiUrl,
        string casinoId,
        string apiToken,
        string uuid,
        string playerCode,
        CancellationToken cancellationToken = default)
        => await _client.KickPlayerAsync(new()
        {
            ApiUrl = apiUrl.ToString(),
            CasinoId = casinoId,
            ApiToken = apiToken,
            Uuid = uuid,
            PlayerCode = playerCode
        }, cancellationToken: cancellationToken);
}
