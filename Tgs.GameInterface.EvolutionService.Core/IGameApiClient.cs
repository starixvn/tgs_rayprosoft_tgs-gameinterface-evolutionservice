using Tgs.GameInterface.EvolutionService.Core.Models;

namespace Tgs.GameInterface.EvolutionService.Core;

public interface IGameApiClient
{
    public ValueTask<CreditResponse> CreditAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        decimal amount,
        string eTransId,
        string createUser,
        CancellationToken cancellationToken = default);

    public ValueTask<DebitResponse> DebitAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        decimal amount,
        string eTransId,
        CancellationToken cancellationToken = default);

    public ValueTask<BalanceResponse> GetBalanceAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        CancellationToken cancellationToken = default);

    public ValueTask<string> GetGameHistoryAsync(
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
        CancellationToken cancellationToken = default);

    public IAsyncEnumerable<GameHistoryInfo> GetGameHistoryAsync(
        Uri apiUrl,
        string authorization,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        CancellationToken cancellationToken = default);

    public ValueTask<GameUrlResponse> GetGameUrlAsync(
        Uri apiUrl,
        GameUrlRequest request,
        CancellationToken cancellationToken = default);

    public ValueTask<PlayerInfoResponse> GetPlayerInfoAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        CancellationToken cancellationToken = default);

    public ValueTask<TransactionInfoResponse> GetTransactionInfoAsync(
        Uri apiUrl,
        string cCode,
        string ecId,
        string euId,
        string eTransId,
        CancellationToken cancellationToken = default);

    public ValueTask<bool> KickPlayerAsync(
        Uri apiUrl,
        string casinoId,
        string apiToken,
        string uuid,
        string playerCode,
        CancellationToken cancellationToken = default);
}
