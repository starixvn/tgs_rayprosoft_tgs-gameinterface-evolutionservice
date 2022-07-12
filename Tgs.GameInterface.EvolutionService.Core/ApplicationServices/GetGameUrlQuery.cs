namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetGameUrlQuery(
    Uri Ua2AuthenticationUrl,
    Uri ApiUrl,
    string PlayerCode,
    GameType GameType,
    PlatformType PlatformType,
    string IpAddress,
    string EncryptedToken,
    string ExternalCultureCode,
    string ExternalCurrencyCode,
    string ExternalGameId);