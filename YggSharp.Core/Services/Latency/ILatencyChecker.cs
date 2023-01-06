using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.Services.Latency;

public interface ILatencyChecker
{
    Task CheckAndFillLatency(IEnumerable<Peer> peers);
}