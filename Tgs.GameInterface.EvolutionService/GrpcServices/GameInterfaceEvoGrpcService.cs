using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Tgs.GameInterface.EvolutionService.Core;
using Tgs.GameInterface.EvolutionService.Core.ApplicationServices;
using Tgs.GameInterface.EvolutionService.Protos;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.GrpcServices;

public class GameInterfaceEvoGrpcService : GameInterfaceEvoService.GameInterfaceEvoServiceBase
{
    private readonly IGetService<CreditQuery, CreditResult> _creditService;
    private readonly IGetService<DebitQuery, DebitResult> _debitService;
    private readonly IGetService<GetBalanceQuery, BalanceResult> _getBalanceService;
    private readonly IGetService<GetGameHistoriesQuery, GameHistoriesResult> _getGameHistoryService;
    private readonly IGetService<GetLiveDealerGameUrlQuery, GameUrlResult> _getLiveDealerGameUrlService;
    private readonly IGetService<GetPlayerInfoQuery, PlayerInfoResult> _getPlayerInfoService;
    private readonly IGetService<GetSlotGameUrlQuery, GameUrlResult> _getSlotGameUrlService;
    private readonly IGetService<GetTransactionInfoQuery, TransactionInfoResult> _getTransactionInfoService;
    private readonly IGetService<KickPlayerQuery, bool> _kickPlayerService;
    private readonly ILogger<GameInterfaceEvoGrpcService> _logger;

    public GameInterfaceEvoGrpcService(
        ILogger<GameInterfaceEvoGrpcService> logger,
        IGetService<GetLiveDealerGameUrlQuery, GameUrlResult> getLiveDealerGameUrlService,
        IGetService<GetSlotGameUrlQuery, GameUrlResult> getSlotGameUrlService,
        IGetService<CreditQuery, CreditResult> creditService,
        IGetService<DebitQuery, DebitResult> debitService,
        IGetService<GetBalanceQuery, BalanceResult> getBalanceService,
        IGetService<GetPlayerInfoQuery, PlayerInfoResult> getPlayerInfoService,
        IGetService<GetTransactionInfoQuery, TransactionInfoResult> getTransactionInfoService,
        IGetService<KickPlayerQuery, bool> kickPlayerService,
        IGetService<GetGameHistoriesQuery, GameHistoriesResult> getGameHistoryService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _getLiveDealerGameUrlService = getLiveDealerGameUrlService ?? throw new ArgumentNullException(nameof(getLiveDealerGameUrlService));
        _getSlotGameUrlService = getSlotGameUrlService ?? throw new ArgumentNullException(nameof(getSlotGameUrlService));
        _creditService = creditService ?? throw new ArgumentNullException(nameof(creditService));
        _debitService = debitService ?? throw new ArgumentNullException(nameof(debitService));
        _getBalanceService = getBalanceService ?? throw new ArgumentNullException(nameof(getBalanceService));
        _getPlayerInfoService = getPlayerInfoService ?? throw new ArgumentNullException(nameof(getPlayerInfoService));
        _getTransactionInfoService = getTransactionInfoService ?? throw new ArgumentNullException(nameof(getTransactionInfoService));
        _kickPlayerService = kickPlayerService ?? throw new ArgumentNullException(nameof(kickPlayerService));
        _getGameHistoryService = getGameHistoryService ?? throw new ArgumentNullException(nameof(getGameHistoryService));
    }

    public override Task<CheckingServiceReply> CheckingService(CheckingServiceRequest request, ServerCallContext context)
    {
        _logger.LogWarning("LogOn:{logOn} Message: Create a check connection.({machineName})", DateTime.Now.ToString("o"), request.MachineName);

        return Task.FromResult(new CheckingServiceReply
        {
            MachineName = Environment.MachineName
        });
    }

    public override async Task<FundInReply> FundIn(FundInRequest request, ServerCallContext context)
    {
        var apiUrl = new Uri(request.ApiUrl);

        var response = await _debitService.GetAsync(new DebitQuery(
            apiUrl,
            request.AgentId,
            request.PlayerCode,
            request.TransactionId,
            request.Amount));

        return new FundInReply
        {
            Result = response.Result,
            EvoTransactionId = response.EvoTransactionId,
            TransactionId = response.TransactionId,
            PlayerCode = response.PlayerCode,
            Uid = response.Uid,
            Balance = response.Balance,
            ErrorMessage = response.ErrorMessage ?? string.Empty
        };
    }

    public override async Task<FundOutReply> FundOut(FundOutRequest request, ServerCallContext context)
    {
        var apiUrl = new Uri(request.ApiUrl);

        var response = await _creditService.GetAsync(new CreditQuery(
            apiUrl,
            request.AgentId,
            request.PlayerCode,
            request.TransactionId,
            request.Amount));

        return new FundOutReply
        {
            Result = response.Result,
            EvoTransactionId = response.EvoTransactionId,
            TransactionId = response.TransactionId,
            PlayerCode = response.PlayerCode,
            Uid = response.Uid,
            Balance = response.Balance,
            ErrorMessage = response.ErrorMessage ?? string.Empty
        };
    }

    public override async Task<GetBalanceReply> GetBalance(GetBalanceRequest request, ServerCallContext context)
    {
        var apiUrl = new Uri(request.ApiUrl);

        var response = await _getBalanceService.GetAsync(new GetBalanceQuery(
            apiUrl,
            request.AgentId,
            request.PlayerCode));

        return new GetBalanceReply
        {
            Result = response.Result,
            PlayerCode = response.PlayerCode,
            Uid = response.Uid,
            Balance = response.ABalance,
            IsPlayerExist = response.IsPlayerExist,
            ErrorMessage = response.ErrorMessage ?? string.Empty
        };
    }

    public override async Task<GetGameHistoriesReply> GetGameHistories(GetGameHistoriesRequest request, ServerCallContext context)
    {
        var apiUrl = new Uri(request.ApiUrl);

        var response = await _getGameHistoryService.GetAsync(new GetGameHistoriesQuery(
            apiUrl,
            request.CasinoId,
            request.GameHistoryApiToken,
            request.StartDate.ToDateTimeOffset(),
            request.EndDate.ToDateTimeOffset()));

        return new GetGameHistoriesReply
        {
            Records = {response.GameHistoryRecords.Select(x => new Protos.GameHistoryRecord
            {
                PlayerCode = x.PlayerCode,
                ExternalRoundId = x.ExternalRoundId,
                ExternalGameId = x.ExternalGameId,
                ExternalCreatedOn = x.ExternalCreatedOn.ToTimestamp(),
                ExternalClosedOn = x.ExternalClosedOn.ToTimestamp(),
                BetAmount = x.BetAmount,
                ValidBetAmount = x.ValidBetAmount,
                SettleAmount = x.SettleAmount,
                AdditionalData = x.AdditionalData
            })}
        };
    }

    public override async Task<GameUrlReply> GetLiveDealerGameUrl(GetLiveDealerGameUrlRequest request, ServerCallContext context)
    {
        var ua2AuthenticationUrl = new Uri(request.Ua2AuthenticationUrl);
        var platformType = (PlatformType)request.PlatformType;

        var response = await _getLiveDealerGameUrlService.GetAsync(new GetLiveDealerGameUrlQuery(
            ua2AuthenticationUrl,
            request.BrandCode,
            request.PlayerCode,
            platformType,
            request.IpAddress,
            request.EncryptedToken,
            request.ExternalCultureCode,
            request.ExternalCurrencyCode));

        return new GameUrlReply
        {
            Entry = response.Entry ?? string.Empty,
            EntryEmbedded = response.EntryEmbedded ?? string.Empty,
            Errors = { response.ErrorMessages?.Select(x => new Error
            {
                Code = x.Code,
                Message = x.Message
            }) ?? new RepeatedField<Error>() }
        };
    }

    public override async Task<GetPlayerInfoReply> GetPlayerInfo(GetPlayerInfoRequest request, ServerCallContext context)
    {
        var apiUrl = new Uri(request.ApiUrl);

        var response = await _getPlayerInfoService.GetAsync(new GetPlayerInfoQuery(
            apiUrl,
            request.AgentId,
            request.PlayerCode));

        return new GetPlayerInfoReply
        {
            EmailAddress = response.EmailAddress,
            PlayerCode = response.PlayerCode,
            Uid = response.Uid,
            Firstname = response.Firstname,
            Lastname = response.Lastname,
            ScreenName = response.ScreenName,
            CountryCode = response.CountryCode,
            ErrorMessage = response.ErrorMessage ?? string.Empty
        };
    }

    public override async Task<GameUrlReply> GetSlotGameUrl(GetSlotGameUrlRequest request, ServerCallContext context)
    {
        var ua2AuthenticationUrl = new Uri(request.Ua2AuthenticationUrl);
        var platformType = (PlatformType)request.PlatformType;

        var response = await _getSlotGameUrlService.GetAsync(new GetSlotGameUrlQuery(
            ua2AuthenticationUrl,
            request.BrandCode,
            request.PlayerCode,
            platformType,
            request.IpAddress,
            request.EncryptedToken,
            request.ExternalCultureCode,
            request.ExternalCurrencyCode,
            request.ExternalGameId));

        return new GameUrlReply
        {
            Entry = response.Entry ?? string.Empty,
            EntryEmbedded = response.EntryEmbedded ?? string.Empty,
            Errors = { response.ErrorMessages?.Select(x => new Error
            {
                Code = x.Code,
                Message = x.Message
            }) ?? new RepeatedField<Error>() }
        };
    }

    public override async Task<GetTransactionInfoReply> GetTransactionInfo(GetTransactionInfoRequest request, ServerCallContext context)
    {
        var apiUrl = new Uri(request.ApiUrl);

        var response = await _getTransactionInfoService.GetAsync(new GetTransactionInfoQuery(
            apiUrl,
            request.AgentId,
            request.PlayerCode,
            request.TransactionId));

        return new GetTransactionInfoReply
        {
            Result = response.Result,
            Balance = response.Balance,
            TransactionId = response.TransactionId,
            EvoTransactionId = response.EvoTransactionId,
            EvoUid = response.EvoUid,
            CCid = response.CCid,
            CreatedOn = DateTime.SpecifyKind(response.CreatedOn, DateTimeKind.Utc).ToTimestamp(),
            CreatedOnIso = response.CreatedOnISO.ToTimestamp(),
            PlayerCode = response.PlayerCode,
            Amount = response.Amount,
            ErrorMessage = response.ErrorMessage ?? string.Empty
        };
    }

    public override async Task<KickPlayerReply> KickPlayer(KickPlayerRequest request, ServerCallContext context)
    {
        var apiUrl = new Uri(request.ApiUrl);

        var response = await _kickPlayerService.GetAsync(new KickPlayerQuery(
            apiUrl,
            request.CasinoId,
            request.ApiToken,
            request.Uuid,
            request.PlayerCode));

        return new KickPlayerReply
        {
            Result = response
        };
    }
}
