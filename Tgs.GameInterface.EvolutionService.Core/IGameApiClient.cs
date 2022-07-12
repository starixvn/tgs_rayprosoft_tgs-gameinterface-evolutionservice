using Tgs.GameInterface.EvolutionService.Core.Models;

namespace Tgs.GameInterface.EvolutionService.Core;

public interface IGameApiClient {
    public ValueTask<GameUrlResponse> GetGameUrlAsync(Uri apiUrl, GameUrlRequest request,CancellationToken cancellationToken);
    public ValueTask<BalanceResponse> GetBalanceAsync(Uri apiUrl, string cCode, string ecId, string euId, CancellationToken cancellationToken);
    public ValueTask<CreditResponse> CreditAsync(Uri apiUrl, string cCode, string ecId, string euId, decimal amount,
        string eTransId, string createUser, CancellationToken cancellationToken);
    public ValueTask<DebitResponse> DebitAsync(Uri apiUrl, string cCode, string ecId, string euId, decimal amount,
        string eTransId, CancellationToken cancellationToken);
    public ValueTask<PlayerInfoResponse> GetPlayerInfoAsync(Uri apiUrl, string cCode, string ecId, string euId, CancellationToken cancellationToken);
    public ValueTask<TransactionInfoResponse> GetTransactionInfoAsync(Uri apiUrl, string cCode, string ecId, string euId,string eTransId, CancellationToken cancellationToken);
    public ValueTask<bool> KickPlayerAsync(Uri apiUrl, string casinoId, string apiToken, CancellationToken cancellationToken);
}