using Tgs.GameInterface.EvolutionService.Core.Models;
using Tgs.GameInterface.EvolutionService.Core.Models.GameUrl;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

internal class GetSlotGameUrlQueryService(IGameApiClient client) : IGetService<GetSlotGameUrlQuery, GameUrlResult>
{
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
                Group = default
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
                },
                Brand = new GameUrlConfigBrand
                {
                    Id = query.BrandCode,
                    Skin = query.BrandCode
                }
            }
        };

        var result = await client.GetGameUrlAsync(query.Ua2AuthenticationUrl, data, cancellationToken).ConfigureAwait(false);

        return new GameUrlResult(
            result.Entry,
            result.EntryEmbedded,
            result.Errors);
    }
}
