using Tgs.GameInterface.EvolutionService.Protos;

namespace Tgs.GameInterface.EvolutionService.Client;

public interface IGameInterfaceEvolutionClient
{
    ValueTask<GameUrlReply> GetLiveDealerGameUrlAsync(
        Uri ua2AuthenticationUrl,
        string playerCode,
        short platformType,
        string ipAddress,
        string encryptedToken,
        string externalCultureCode,
        string externalCurrencyCode,
        string externalGameId,
        CancellationToken cancellationToken = default);

    ValueTask<GameUrlReply> GetSlotGameUrlAsync(
        Uri ua2AuthenticationUrl,
        string playerCode,
        short platformType,
        string ipAddress,
        string encryptedToken,
        string externalCultureCode,
        string externalCurrencyCode,
        string externalGameId,
        CancellationToken cancellationToken = default);

    ValueTask<FundInReply> FundInAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        string transactionId,
        decimal amount,
        CancellationToken cancellationToken = default);

    ValueTask<FundOutReply> FundOutAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        string transactionId,
        decimal amount,
        CancellationToken cancellationToken = default);

    ValueTask<GetBalanceReply> GetBalanceAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        CancellationToken cancellationToken = default);

    ValueTask<GetPlayerInfoReply> GetPlayerInfoAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        CancellationToken cancellationToken = default);

    ValueTask<GetTransactionInfoReply> GetTransactionInfoAsync(
        Uri apiUrl,
        string agentId,
        string playerCode,
        string transactionId,
        CancellationToken cancellationToken = default);

    ValueTask<KickPlayerReply> KickPlayerAsync(
        Uri apiUrl,
        string casinoId,
        string apiToken,
        string uuid,
        string playerCode,
        CancellationToken cancellationToken = default);

    ValueTask<GetGameHistoriesReply> GetGameHistoriesAsync(
        Uri apiUrl,
        string authorization,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        CancellationToken cancellationToken = default);

}