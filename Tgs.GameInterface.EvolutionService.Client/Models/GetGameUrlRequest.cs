namespace Tgs.GameInterface.EvolutionService.Client.Models;

public record GetGameUrlRequest(
    Uri Ua2AuthenticationUrl,
    Uri ApiUrl,
    string PlayerCode,
    short GameType,
    short PlatformType,
    string IpAddress,
    string EncryptedToken,
    string ExternalCultureCode,
    string ExternalCurrencyCode,
    string ExternalGameId);