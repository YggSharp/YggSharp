using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.LatencyCheckerService;

public interface ILatencyChecker
{
    /// <summary>
    /// Checks latency for each peer in collection
    /// </summary>
    /// <param name="peers">Peer collection</param>
    /// <param name="parallelism">Degree of parallelism</param>
    /// <param name="passes">Number of checks for each peer</param>
    /// <returns></returns>
    Task CheckLatency(IEnumerable<Peer> peers, int parallelism = 8, int passes = 1);
}