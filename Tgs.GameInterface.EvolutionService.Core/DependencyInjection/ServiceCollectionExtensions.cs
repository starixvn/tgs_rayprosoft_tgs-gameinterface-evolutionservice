using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tgs.GameInterface.EvolutionService.Core;

namespace Tgs.GameInterface.EvolutionService.Core.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEvoServiceCore(this IServiceCollection services)
        {
            services
                .RegisterAllServiceUtilityTypes(
                    Assembly.GetExecutingAssembly(),
                    ServiceLifetime.Transient)
                .AddHttpClient<IGameApiClient, GameApiClient>();

            return services;
        }
    }
}
