using System.Runtime.Serialization;

namespace Tgs.GameInterface.EvolutionService.Grpc.Models;

[DataContract]
public class GameUrlResponse
{
    [DataMember(Order = 1)]
    public string? GameUrl { get; set; }

    [DataMember(Order = 2)]
    public Error[]? ErrorMessages { get; set; }
}