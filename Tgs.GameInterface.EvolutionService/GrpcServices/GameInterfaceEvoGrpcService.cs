using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Tgs.GameInterface.EvolutionService.Core;
using Tgs.GameInterface.EvolutionService.Core.ApplicationServices;
using Tgs.GameInterface.EvolutionService.Protos;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.GrpcServices
{
    public class GameInterfaceEvoGrpcService : GameInterfaceEvoService.GameInterfaceEvoServiceBase
    {
        private readonly IGetService<GetGameUrlQuery, GameUrlResult> _getGameUrlService;
        private readonly IGetService<CreditQuery, CreditResult> _creditService;
        private readonly IGetService<DebitQuery, DebitResult> _debitService;
        private readonly IGetService<GetBalanceQuery, BalanceResult> _getBalanceService;
        private readonly IGetService<GetPlayerInfoQuery, PlayerInfoResult> _getPlayerInfoService;
        private readonly IGetService<GetTransactionInfoQuery, TransactionInfoResult> _getTransactionInfoService;
        private readonly IGetService<KickPlayerQuery, bool> _kickPlayerService;

        public GameInterfaceEvoGrpcService(
            IGetService<GetGameUrlQuery, GameUrlResult> getGameUrlService,
            IGetService<CreditQuery, CreditResult> creditService,
            IGetService<DebitQuery, DebitResult> debitService,
            IGetService<GetBalanceQuery, BalanceResult> getBalanceService,
            IGetService<GetPlayerInfoQuery, PlayerInfoResult> getPlayerInfoService,
            IGetService<GetTransactionInfoQuery, TransactionInfoResult> getTransactionInfoService,
            IGetService<KickPlayerQuery, bool> kickPlayerService)
        {
            _getGameUrlService = getGameUrlService ?? throw new ArgumentNullException(nameof(getGameUrlService));
            _creditService = creditService ?? throw new ArgumentNullException(nameof(creditService));
            _debitService = debitService ?? throw new ArgumentNullException(nameof(debitService));
            _getBalanceService = getBalanceService ?? throw new ArgumentNullException(nameof(getBalanceService));
            _getPlayerInfoService = getPlayerInfoService ?? throw new ArgumentNullException(nameof(getPlayerInfoService));
            _getTransactionInfoService = getTransactionInfoService ?? throw new ArgumentNullException(nameof(getTransactionInfoService));
            _kickPlayerService = kickPlayerService ?? throw new ArgumentNullException(nameof(kickPlayerService));
        }

        public override async Task<GameUrlReply> GetGameUrl(GetGameUrlRequest request, ServerCallContext context)
        {
            var ua2AuthenticationUrl = new Uri(request.Ua2AuthenticationUrl);
            var apiUrl = new Uri(request.ApiUrl);
            var gameType = (GameType)request.GameType;
            var platformType = (PlatformType)request.PlatformType;

            var response = await _getGameUrlService.GetAsync(new GetGameUrlQuery(
                ua2AuthenticationUrl,
                apiUrl,
                request.PlayerCode,
                gameType,
                platformType,
                request.IpAddress,
                request.EncryptedToken,
                request.ExternalCultureCode,
                request.ExternalCurrencyCode,
                request.ExternalGameId));

            return new GameUrlReply
            {
                Url = response.GameUrl,
                Errors = { response.ErrorMessages?.Select(x => new Error
                {
                    Code = x.Code,
                    Message = x.Message
                }) }
            };
        }

        public override async Task<FundInReply> FundIn(FundInRequest request, ServerCallContext context)
        {
            var apiUrl = new Uri(request.ApiUrl);

            var response = await _creditService.GetAsync(new CreditQuery(
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
                ErrorMessage = response.ErrorMessage
            };
        }

        public override async Task<FundOutReply> FundOut(FundOutRequest request, ServerCallContext context)
        {
            var apiUrl = new Uri(request.ApiUrl);

            var response = await _debitService.GetAsync(new DebitQuery(
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
                ErrorMessage = response.ErrorMessage
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
                ABalance = response.ABalance,
                TBalance = response.TBalance,
                ErrorMessage = response.ErrorMessage
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
                ErrorMessage = response.ErrorMessage
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
                CreatedOn = response.CreatedOn.ToTimestamp(),
                CreatedOnIso = response.CreatedOnISO.ToTimestamp(),
                PlayerCode = response.PlayerCode,
                Amount = response.Amount,
                ErrorMessage = response.ErrorMessage
            };
        }

        public override async Task<KickPlayerReply> KickPlayer(KickPlayerRequest request, ServerCallContext context)
        {
            var apiUrl = new Uri(request.ApiUrl);

            var response = await _kickPlayerService.GetAsync(new KickPlayerQuery(
                apiUrl,
                request.CasinoId,
                request.ApiToken));

            return new KickPlayerReply
            {
                Result = response
            };
        }

    }
}
