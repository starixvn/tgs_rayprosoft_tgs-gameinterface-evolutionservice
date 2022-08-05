using System.Runtime.Serialization;

namespace Tgs.GameInterface.EvolutionService.Grpc.Models
{
    [DataContract]
    public class Error
    {
        [DataMember(Order = 1)]
        public string Code { get; set; } = default!;

        [DataMember(Order = 2)]
        public string Message { get; set; } = default!;
    }
}
