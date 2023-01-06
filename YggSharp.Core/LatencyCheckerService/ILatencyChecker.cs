using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.LatencyCheckerService;

public interface ILatencyChecker
{
    Task CheckLatency(List<Peer> peers);
}