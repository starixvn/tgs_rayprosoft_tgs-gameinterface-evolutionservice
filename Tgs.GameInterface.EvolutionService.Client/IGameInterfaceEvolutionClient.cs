using System.Text;
using System.Text.Json;
using Tgs.GameInterface.EvolutionService;
using Tgs.GameInterface.EvolutionService.Protos;

namespace Tgs.GameInterface.EvolutionService.Client
{
    public interface IGameInterfaceEvolutionClient
    {
        ValueTask<GameUrlReply> GetGameUrlAsync(
            Uri ua2AuthenticationUrl,
            Uri apiUrl,
            string playerCode,
            short gameType,
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

        ValueTask<GetPlayerInfoReply> GetPlayerInfoAsync(Uri apiUrl,
            string agentId,
            string playerCode,
            CancellationToken cancellationToken = default);

        ValueTask<GetTransactionInfoReply> GetTransactionInfoAsync(Uri apiUrl,
            string agentId,
            string playerCode,
            string transactionId,
            CancellationToken cancellationToken = default);
    }
}