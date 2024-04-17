using Tgs.GameInterface.EvolutionService.Core;

namespace Tgs.GameInterface.EvolutionService.Models;

public record GetLiveDealerLobbyGameUrlRequest(
	Uri Ua2AuthenticationUrl,
	string BrandCode,
	string PlayerCode,
	PlatformType PlatformType,
	string IpAddress,
	string EncryptedToken,
	string ExternalCultureCode,
	string ExternalCurrencyCode);
