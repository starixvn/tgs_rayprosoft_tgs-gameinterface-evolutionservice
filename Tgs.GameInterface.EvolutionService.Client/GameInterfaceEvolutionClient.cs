using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Tgs.GameInterface.EvolutionService.Protos;
using GetGameUrlRequest = Tgs.GameInterface.EvolutionService.Protos.GetGameUrlRequest;

namespace Tgs.GameInterface.EvolutionService.Client
{
    public class GameInterfaceEvolutionClient : IGameInterfaceEvolutionClient
    {
        private readonly GameInterfaceEvoService.GameInterfaceEvoServiceClient _client;

        public GameInterfaceEvolutionClient(GameInterfaceEvoService.GameInterfaceEvoServiceClient client)
        {
            _client = client;
        }

        public async ValueTask<GameUrlReply> GetGameUrlAsync(
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
            CancellationToken cancellationToken = default)
        {
            return await _client.GetGameUrlAsync(new GetGameUrlRequest
            {
                Ua2AuthenticationUrl = ua2AuthenticationUrl.ToString(),
                ApiUrl = apiUrl.ToString(),
                PlayerCode = playerCode,
                GameType = gameType,
                PlatformType = platformType,
                IpAddress = ipAddress,
                EncryptedToken = encryptedToken,
                ExternalCultureCode = externalCultureCode,
                ExternalCurrencyCode= externalCurrencyCode,
                ExternalGameId= externalGameId
            },cancellationToken: cancellationToken);
        }

        public async ValueTask<FundInReply> FundInAsync(Uri apiUrl, string agentId, string playerCode, string transactionId, decimal amount, CancellationToken cancellationToken = default) 
            => await _client.FundInAsync(new FundInRequest
        {
            ApiUrl = apiUrl.ToString(),
            AgentId = agentId,
            PlayerCode = playerCode,
            TransactionId = transactionId,
            Amount = amount
        },cancellationToken: cancellationToken);

        public async ValueTask<FundOutReply> FundOutAsync(Uri apiUrl, string agentId, string playerCode, string transactionId, decimal amount,
            CancellationToken cancellationToken = default)
            => await _client.FundOutAsync(new FundOutRequest
            {
                ApiUrl = apiUrl.ToString(),
                AgentId = agentId,
                PlayerCode = playerCode,
                TransactionId = transactionId,
                Amount = amount
            }, cancellationToken: cancellationToken);

        public async ValueTask<GetBalanceReply> GetBalanceAsync(Uri apiUrl, string agentId, string playerCode, CancellationToken cancellationToken = default)
            => await _client.GetBalanceAsync(new GetBalanceRequest
            {
                ApiUrl = apiUrl.ToString(),
                AgentId = agentId,
                PlayerCode = playerCode
            }, cancellationToken: cancellationToken);

        public async ValueTask<GetPlayerInfoReply> GetPlayerInfoAsync(Uri apiUrl, string agentId, string playerCode,
            CancellationToken cancellationToken = default)
            => await _client.GetPlayerInfoAsync(new GetPlayerInfoRequest
            {
                ApiUrl = apiUrl.ToString(),
                AgentId = agentId,
                PlayerCode = playerCode
            }, cancellationToken: cancellationToken);

        public async ValueTask<GetTransactionInfoReply> GetTransactionInfoAsync(Uri apiUrl, string agentId, string playerCode, string transactionId,
            CancellationToken cancellationToken = default)
            => await _client.GetTransactionInfoAsync(new GetTransactionInfoRequest
            {
                ApiUrl = apiUrl.ToString(),
                AgentId = agentId,
                PlayerCode = playerCode,
                TransactionId = transactionId,
            }, cancellationToken: cancellationToken);
    }
}