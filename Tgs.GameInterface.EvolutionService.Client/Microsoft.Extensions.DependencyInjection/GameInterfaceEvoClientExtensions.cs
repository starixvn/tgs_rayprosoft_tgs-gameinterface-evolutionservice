using Grpc.Core;
using Grpc.Net.Client.Balancer;
using Microsoft.Extensions.Options;
using Tgs.GameInterface.EvolutionService.Client;
using Tgs.GameInterface.EvolutionService.Protos;

namespace Microsoft.Extensions.DependencyInjection;

public static class GameInterfaceEvoClientExtensions
{
    public static IServiceCollection AddGameInterfaceEvoClient(this IServiceCollection services,
        Action<GameInterfaceEvolutionClientOptions, IServiceProvider> configure)
    {
        var isDns = false;
        _ = services
            .AddOptions<GameInterfaceEvolutionClientOptions>()
            .Configure(configure)
            .Services
            .AddGrpcClient<GameInterfaceEvoService.GameInterfaceEvoServiceClient>((sp, configureClient) =>
            {
                var options = sp.GetRequiredService<IOptions<GameInterfaceEvolutionClientOptions>>().Value;
                isDns = options.BaseUrl.Scheme.Equals("dns", StringComparison.OrdinalIgnoreCase);
                configureClient.Address = options.BaseUrl;
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
                configureChannel.Credentials = isDns
                ? ChannelCredentials.Insecure
                : null;
            })
            .AddInterceptor<LoggingInterceptor>()
            .Services
            .AddSingleton<DiagnosticsInterceptorOptions>()
            .AddSingleton<LoggingInterceptor>()
            .AddTransient<IGameInterfaceEvolutionClient, GameInterfaceEvolutionClient>();

        if (isDns)
        {
            //負載平衡時，效能很重要。 藉由快取位址，解析位址的延遲會從 gRPC 呼叫中排除。 進行第一個 gRPC 呼叫時，將會叫用解析程式，而後續的呼叫會使用快取。
            _ = services.AddSingleton<ResolverFactory>(sp => new DnsResolverFactory(refreshInterval: TimeSpan.FromSeconds(30)));
        }

        return services;
    }
}
