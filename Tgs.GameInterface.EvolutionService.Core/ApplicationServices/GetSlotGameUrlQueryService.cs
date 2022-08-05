using Tgs.GameInterface.EvolutionService.Core.Models;
using Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices
{
    internal class GetSlotGameUrlQueryService : IGetService<GetSlotGameUrlQuery, GameUrlResult>
    {
        private readonly IGameApiClient _client;

        public GetSlotGameUrlQueryService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask<GameUrlResult> GetAsync(GetSlotGameUrlQuery query, CancellationToken cancellationToken = default)
        {
            var data = new GameUrlRequest
            {
                Uuid = Guid.NewGuid().ToString(),
                Player = new GameUrlPlayer
                {
                    Id = query.PlayerCode,
                    Update = false,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Nickname = query.PlayerCode,
                    Country = "CN",
                    Language = query.ExternalCultureCode,
                    Currency = query.ExternalCurrencyCode,
                    Session = new GameUrlPlayerSession
                    {
                        Id = query.EncryptedToken,
                        Ip = query.IpAddress
                    },
                    Group = default(GameUrlPlayerGroup)
                },
                Config = new GameUrlConfig
                {
                    Game = new GameUrlConfigGame
                    {
                        Category = "slots",
                        Table = new GameUrlConfigGameTable
                        {
                            Id = query.ExternalGameId
                        }
                    },
                    Channel = new GameUrlConfigChannel
                    {
                        Wrapped = true,
                        Mobile = query.PlatformType == PlatformType.Mobile
                    }
                }
            };

            var result = await _client.GetGameUrlAsync(query.Ua2AuthenticationUrl, data, cancellationToken).ConfigureAwait(false);

            return new GameUrlResult(
                result.Entry,
                result.EntryEmbedded,
                result.Errors);
        }
    }
}
