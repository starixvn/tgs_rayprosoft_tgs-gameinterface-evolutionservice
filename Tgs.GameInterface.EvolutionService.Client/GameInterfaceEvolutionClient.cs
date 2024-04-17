using System.Runtime.CompilerServices;
using Google.Protobuf.WellKnownTypes;
using Tgs.GameInterface.EvolutionService.Protos;

namespace Tgs.GameInterface.EvolutionService.Client;

public class GameInterfaceEvolutionClient(GameInterfaceEvoService.GameInterfaceEvoServiceClient client) : IGameInterfaceEvolutionClient
{
	public async ValueTask<string> CheckingServiceAsync(string machineName, CancellationToken cancellationToken = default)
	{
		var request = new CheckingServiceRequest
		{
			MachineName = machineName,
		};

		var reply = await client
			.CheckingServiceAsync(
				request: request,
				cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		return reply.MachineName;
	}

	public async ValueTask<FundInReply> FundInAsync(
		Uri apiUrl,
		string agentId,
		string playerCode,
		string transactionId,
		decimal amount, CancellationToken cancellationToken = default)
		=> await client.FundInAsync(new()
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
		=> await client.FundOutAsync(new()
		{
			ApiUrl = apiUrl.ToString(),
			AgentId = agentId,
			PlayerCode = playerCode,
			TransactionId = transactionId,
			Amount = amount
		}, cancellationToken: cancellationToken);

	public async IAsyncEnumerable<GameRecordInfo> GameRecordAsync(
		Uri apiUrl,
		string casinoId,
		string gameHistoryApiToken,
		DateTimeOffset startTime,
		DateTimeOffset endTime,
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var request = new GameRecordRequest
		{
			ApiUrl = apiUrl.ToString(),
			CasinoId = casinoId,
			GameHistoryApiToken = gameHistoryApiToken,
			StartTime = startTime.ToTimestamp(),
			EndTime = endTime.ToTimestamp()
		};
		var reply = await client
			.GameRecordAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		var gameRecords = reply.Items
				   ?? Enumerable.Empty<GameRecordInfo>();

		if (!gameRecords.Any())
		{
			yield break;
		}

		foreach (var gameRecord in gameRecords)
		{
			yield return gameRecord;
		}
	}

	public async ValueTask<GetBalanceReply> GetBalanceAsync(
		Uri apiUrl,
		string agentId,
		string playerCode,
		CancellationToken cancellationToken = default)
		=> await client.GetBalanceAsync(new()
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
		=> await client.GetGameHistoriesAsync(new()
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
		=> await client.GetLiveDealerGameUrlAsync(new()
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
		=> await client.GetPlayerInfoAsync(new()
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
		=> await client.GetSlotGameUrlAsync(new()
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
		=> await client.GetTransactionInfoAsync(new()
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
		=> await client.KickPlayerAsync(new()
		{
			ApiUrl = apiUrl.ToString(),
			CasinoId = casinoId,
			ApiToken = apiToken,
			Uuid = uuid,
			PlayerCode = playerCode
		}, cancellationToken: cancellationToken);
}
