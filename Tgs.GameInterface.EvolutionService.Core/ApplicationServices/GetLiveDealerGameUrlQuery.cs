namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record GetLiveDealerGameUrlQuery(
    Uri Ua2AuthenticationUrl,
    string BrandCode,
    string PlayerCode,
    PlatformType PlatformType,
    string IpAddress,
    string EncryptedToken,
    string ExternalCultureCode,
    string ExternalCurrencyCode);