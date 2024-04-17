namespace Tgs.GameInterface.EvolutionService.Core.ApplicationServices;

public record PlayerInfoResult(
	string EmailAddress,
	string Firstname,
	string Lastname,
	string ScreenName,
	string CountryCode,
	string PlayerCode,
	string Uid,
	string? ErrorMessage);
