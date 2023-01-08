using Microsoft.Extensions.DependencyInjection;
using YggSharp.Admin;
using YggSharp.Core.Services.Latency;
using YggSharp.Core.Services.Peers;
using YggSharp.Core.Services.Yggdrasil;

namespace YggSharp.Core.Services;

public static class ServiceExtensions
{
    public static void AddYggdrasilServices(this IServiceCollection services)
    {
        services.AddSingleton<ILatencyChecker, LatencyChecker>();
        services.AddSingleton<IPeerParserService, PeerParserService>();
        services.AddSingleton<IYggdrasilService, YggdrasilService>();
        services.AddSingleton<IYggSharpAdminClient>(new YggSharpAdminClient("127.0.0.1", 9001));
    }
}