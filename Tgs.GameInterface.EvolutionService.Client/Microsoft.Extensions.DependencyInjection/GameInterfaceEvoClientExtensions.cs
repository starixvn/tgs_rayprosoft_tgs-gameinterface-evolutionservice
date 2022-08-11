using Microsoft.Extensions.Options;
using Tgs.GameInterface.EvolutionService.Client;
using Tgs.GameInterface.EvolutionService.Protos;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GameInterfaceEvoClientExtensions
    {
        public static IServiceCollection AddGameInterfaceEvoClient(this IServiceCollection services,
            Action<GameInterfaceEvoOptions, IServiceProvider> configure)
        {
            services
                .AddOptions<GameInterfaceEvoOptions>()
                .Configure(configure)
                .Services
                .AddGrpcClient<GameInterfaceEvoService.GameInterfaceEvoServiceClient>((sp, configureClient) =>
                {
                    var options = sp.GetRequiredService<IOptions<GameInterfaceEvoOptions>>().Value;
                    configureClient.Address = options.Url;
                })
                .ConfigureChannel((sp, configureChannel) =>
                {
                    var handler = new SocketsHttpHandler
                    {
                        PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                        KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                        KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                        EnableMultipleHttp2Connections = true
                    };
                    configureChannel.HttpHandler = handler;
                })
                .AddInterceptor<LoggingInterceptor>()
                .Services
                .AddSingleton<DiagnosticsInterceptorOptions>()
                .AddSingleton<LoggingInterceptor>()
                .AddTransient<IGameInterfaceEvolutionClient, GameInterfaceEvolutionClient>(); ;

            return services;
        }
    }
}
