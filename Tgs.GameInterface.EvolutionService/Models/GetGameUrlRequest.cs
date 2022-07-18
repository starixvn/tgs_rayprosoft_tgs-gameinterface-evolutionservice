using Tgs.GameInterface.EvolutionService.Core;

namespace Tgs.GameInterface.EvolutionService.Models;

public record GetGameUrlRequest(
    Uri Ua2AuthenticationUrl,
    string PlayerCode,
    PlatformType PlatformType,
    string IpAddress,
    string EncryptedToken,
    string ExternalCultureCode,
    string ExternalCurrencyCode,
    string ExternalGameId);