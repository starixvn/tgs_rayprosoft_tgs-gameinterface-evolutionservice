namespace Tgs.GameInterface.EvolutionService.Models;

public record PlayerInfoResponse(
    string EmailAddress,
    string Firstname,
    string Lastname,
    string ScreenName,
    string CountryCode,
    string PlayerCode,
    string Uid,
    string? ErrorMessage);
