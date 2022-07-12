using Tgs.GameInterface.EvolutionService.Core;

namespace Tgs.GameInterface.EvolutionService.Models;

public record GetGameUrlRequest(
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