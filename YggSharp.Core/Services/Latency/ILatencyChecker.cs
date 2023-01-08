using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.Services.Latency;

public interface ILatencyChecker
{
    Task CheckAndFillLatency(IEnumerable<Peer> peers, int parallelism = 16, int passes = 1);
}