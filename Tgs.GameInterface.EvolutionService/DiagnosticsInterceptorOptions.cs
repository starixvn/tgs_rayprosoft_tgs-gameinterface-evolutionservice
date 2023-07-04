using System.Diagnostics;

namespace Tgs.GameInterface.EvolutionService;

public class DiagnosticsInterceptorOptions
{
    public ActivitySource InterceptorActivitySource { get; set; } =
        new ActivitySource($"Tgs.GameInterface.EvolutionService.DiagnosticsInterceptor");
}
