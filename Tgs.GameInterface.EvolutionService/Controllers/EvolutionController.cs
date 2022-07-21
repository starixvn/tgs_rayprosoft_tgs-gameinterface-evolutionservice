using Microsoft.AspNetCore.Mvc;
using Tgs.GameInterface.EvolutionService.Core.ApplicationServices;
using Tgs.GameInterface.EvolutionService.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvolutionController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="request"></param>
        /// "apiUrl": "https://babylonpega2.uat1.evo-test.com",
        /// "agentId": "babylonpega20001test123",
        /// "playerCode": "4ucmqplxb029",
        /// "transactionId": "4ucmqplxb029test2",
        /// "amount": 10
        /// <returns></returns>
        [HttpPost("fund-in")]
        public async ValueTask<FundInResponse> FundInAsync(
            [FromServices] IGetService<CreditQuery, CreditResult> service,
            FundInRequest request)
        {
            var result = await service.GetAsync(new CreditQuery(
                request.ApiUrl,
                request.AgentId,
                request.PlayerCode,
                request.TransactionId,
                request.Amount)).ConfigureAwait(false);

            return new FundInResponse(
                result.Result,
                result.EvoTransactionId,
                result.TransactionId,
                result.PlayerCode,
                result.Uid,
                result.Balance,
                result.ErrorMessage);
        }

        [HttpPost("fund-out")]
        public async ValueTask<FundOutResponse> FundOutAsync(
            [FromServices] IGetService<DebitQuery, DebitResult> service,
            FundOutRequest request)
        {
            var result = await service.GetAsync(new DebitQuery(
                request.ApiUrl,
                request.AgentId,
                request.PlayerCode,
                request.TransactionId,
                request.Amount)).ConfigureAwait(false);

            return new FundOutResponse(
                result.Result,
                result.EvoTransactionId,
                result.TransactionId,
                result.PlayerCode,
                result.Uid,
                result.Balance,
                result.ErrorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="request"></param>
        /// "ua2AuthenticationUrl": "https://babylonpega2.uat1.evo-test.com/ua/v1/babylonpega20001/test123",
        ///"apiUrl": "https://babylonpega2.uat1.evo-test.com",
        ///"playerCode": "4ucmqplxb029",
        ///"gameType": "Sportsbook",
        ///"platformType": "Desktop",
        ///"ipAddress": "50.50.50.50",
        ///"encryptedToken": "test123",
        ///"externalCultureCode": "cn",
        ///"externalCurrencyCode": "CNY",
        ///"externalGameId": "dracula000000000"
        /// <returns></returns>
        [HttpPost("liveDealerGameUrl")]
        public async ValueTask<GameUrlResponse> GetLiveDealerGameUrlAsync(
            [FromServices] IGetService<GetLiveDealerGameUrlQuery, GameUrlResult> service,
            GetGameUrlRequest request)
        {
            var result = await service.GetAsync(new GetLiveDealerGameUrlQuery(
                request.Ua2AuthenticationUrl,
                request.PlayerCode,
                request.PlatformType,
                request.IpAddress,
                request.EncryptedToken,
                request.ExternalCultureCode,
                request.ExternalCurrencyCode,
                request.ExternalGameId)).ConfigureAwait(false);

            return new GameUrlResponse(
                result.Entry,
                result.EntryEmbedded,
                result.ErrorMessages);
        }

        [HttpPost("slotGameUrl")]
        public async ValueTask<GameUrlResponse> GetSlotGameUrlAsync(
            [FromServices] IGetService<GetSlotGameUrlQuery, GameUrlResult> service,
            GetGameUrlRequest request)
        {
            var result = await service.GetAsync(new GetSlotGameUrlQuery(
                request.Ua2AuthenticationUrl,
                request.PlayerCode,
                request.PlatformType,
                request.IpAddress,
                request.EncryptedToken,
                request.ExternalCultureCode,
                request.ExternalCurrencyCode,
                request.ExternalGameId)).ConfigureAwait(false);

            return new GameUrlResponse(
                result.Entry,
                result.EntryEmbedded,
                result.ErrorMessages);
        }

        [HttpPost("balance")]
        public async ValueTask<BalanceResponse> GetBalanceAsync(
            [FromServices] IGetService<GetBalanceQuery, BalanceResult> service,
            GetBalanceRequest request)
        {
            var result = await service.GetAsync(new GetBalanceQuery(
                request.ApiUrl,
                request.AgentId,
                request.PlayerCode
            )).ConfigureAwait(false);

            return new BalanceResponse(
                result.Result,
                result.PlayerCode,
                result.Uid,
                result.TBalance,
                result.ABalance,
                result.IsPlayerExist,
                result.ErrorMessage);
        }

        [HttpPost("playerInfo")]
        public async ValueTask<PlayerInfoResponse> GetPlayerInfoAsync(
            [FromServices] IGetService<GetPlayerInfoQuery, PlayerInfoResult> service,
            PlayerInfoRequest request)
        {
            var result = await service.GetAsync(new GetPlayerInfoQuery(
                request.ApiUrl,
                request.AgentId,
                request.PlayerCode
            )).ConfigureAwait(false);

            return new PlayerInfoResponse(
                result.EmailAddress,
                result.Firstname,
                result.Lastname,
                result.ScreenName,
                result.CountryCode,
                result.PlayerCode,
                result.Uid,
                result.ErrorMessage);
        }

        [HttpPost("transactionInfo")]
        public async ValueTask<TransactionInfoResponse> GetTransactionInfoAsync(
            [FromServices] IGetService<GetTransactionInfoQuery, TransactionInfoResult> service,
            TransactionInfoRequest request)
        {
            var result = await service.GetAsync(new GetTransactionInfoQuery(
                request.ApiUrl,
                request.AgentId,
                request.PlayerCode,
                request.TransactionId
            )).ConfigureAwait(false);

            return new TransactionInfoResponse(
                result.Result,
                result.Balance,
                result.TransactionId,
                result.EvoTransactionId,
                result.CreatedOn,
                result.CreatedOnISO,
                result.EvoUid,
                result.PlayerCode,
                result.CCid,
                result.Amount,
                result.ErrorMessage);
        }

        [HttpPost("kickPlayer")]
        public async ValueTask<bool> KickPlayerAsync(
            [FromServices] IGetService<KickPlayerQuery, bool> service,
            KickPlayerRequest request)
        {
            return await service.GetAsync(new KickPlayerQuery(
                request.ApiUrl,
                request.CasinoId,
                request.ApiToken,
                request.Uuid,
                request.PlayerCode
            )).ConfigureAwait(false);
        }

        [HttpPost("gameHistories")]
        public async ValueTask<GameHistoriesResponse> GetGameHistoriesAsync(
            [FromServices] IGetService<GetGameHistoriesQuery, GameHistoriesResult> service,
            GetGameHistoriesRequest request)
        {
            var result = await service.GetAsync(new GetGameHistoriesQuery(
                request.ApiUrl,
                request.Authorization,
                request.StartDate,
                request.EndDate,
                request.GameType,
                request.Channel,
                request.GameProvider,
                request.TransactionId,
                request.OwTransactionId,
                request.Rounding
            )).ConfigureAwait(false);

            return new GameHistoriesResponse
            {
                GameHistoryRecords = result.GameHistoryRecords.Select(x => new GameHistoryRecordResponse
                {
                    PlayerCode = x.PlayerCode,
                    ExternalRoundId = x.ExternalRoundId,
                    ExternalGameId = x.ExternalGameId,
                    ExternalCreatedOn = x.ExternalCreatedOn,
                    ExternalClosedOn = x.ExternalClosedOn,
                    BetAmount = x.BetAmount,
                    ValidBetAmount = x.ValidBetAmount,
                    SettleAmount = x.SettleAmount,
                    AdditionalData = x.AdditionalData
                })
            };
        }

    }
}
