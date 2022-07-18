using System.ServiceModel;
using ProtoBuf.Grpc;
using Tgs.GameInterface.EvolutionService.Grpc.Models;

namespace Tgs.GameInterface.EvolutionService.Grpc.Interface
{
    [ServiceContract]
    public interface IGameInterfaceEvoGrpcService
    {
        Task<GameUrlResponse> GetGameUrl(GetGameUrlRequest request, CallContext context = default);
    }
}
