using System.Text;
using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using Tgs.GameInterface.EvolutionService.Protos;

namespace Tgs.GameInterface.EvolutionService.Client
{
    public class GameInterfaceEvolutionClient : IGameInterfaceEvolutionClient
    {
        private readonly GameInterfaceEvoService.GameInterfaceEvoServiceClient _client;

        public GameInterfaceEvolutionClient(GameInterfaceEvoService.GameInterfaceEvoServiceClient client)
        {
            _client = client;
        }

        public async ValueTask<GameUrlReply> GetLiveDealerGameUrlAsync(
            Uri ua2AuthenticationUrl,
            string playerCode,
            short platformType,
            string ipAddress,
            string encryptedToken,
            string externalCultureCode,
            string externalCurrencyCode,
            string externalGameId, 
            CancellationToken cancellationToken = default)
        {
            return await _client.GetLiveDealerGameUrlAsync(new GetLiveDealerGameUrlRequest
            {
                Ua2AuthenticationUrl = ua2AuthenticationUrl.ToString(),
                PlayerCode = playerCode,
                PlatformType = platformType,
                IpAddress = ipAddress,
                EncryptedToken = encryptedToken,
                ExternalCultureCode = externalCultureCode,
                ExternalCurrencyCode= externalCurrencyCode,
                ExternalGameId= externalGameId
            },cancellationToken: cancellationToken);
        }
        public async ValueTask<GameUrlReply> GetSlotGameUrlAsync(
            Uri ua2AuthenticationUrl,
            string playerCode,
            short platformType,
            string ipAddress,
            string encryptedToken,
            string externalCultureCode,
            string externalCurrencyCode,
            string externalGameId,
            CancellationToken cancellationToken = default)
        {
            return await _client.GetSlotGameUrlAsync(new GetSlotGameUrlRequest
            {
                Ua2AuthenticationUrl = ua2AuthenticationUrl.ToString(),
                PlayerCode = playerCode,
                PlatformType = platformType,
                IpAddress = ipAddress,
                EncryptedToken = encryptedToken,
                ExternalCultureCode = externalCultureCode,
                ExternalCurrencyCode = externalCurrencyCode,
                ExternalGameId = externalGameId
            }, cancellationToken: cancellationToken);
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

        public async ValueTask<KickPlayerReply> KickPlayerAsync(Uri apiUrl, string casinoId, string apiToken, string uuid, string playerCode,
            CancellationToken cancellationToken = default)
            => await _client.KickPlayerAsync(new KickPlayerRequest
            {
                ApiUrl = apiUrl.ToString(),
                CasinoId = casinoId,
                ApiToken = apiToken,
                Uuid = uuid,
                PlayerCode = playerCode,
            }, cancellationToken: cancellationToken);

        public async ValueTask<GetGameHistoriesReply> GetGameHistoriesAsync(Uri apiUrl, string authorization, DateTimeOffset? startDate, DateTimeOffset? endDate,
            string? gameType, string? channel, string? gameProvider, string? transactionId, string? owTransactionId,
            bool? rounding, CancellationToken cancellationToken = default)
            => await _client.GetGameHistoriesAsync(new GetGameHistoriesRequest
            {
                ApiUrl = apiUrl.ToString(),
                Authorization = authorization,
                StartDate = startDate?.ToTimestamp(),
                EndDate = endDate?.ToTimestamp()
            }, cancellationToken: cancellationToken);
    }
}