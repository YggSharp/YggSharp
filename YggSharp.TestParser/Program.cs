using YggSharp.Core.Services.Latency;
using YggSharp.Core.Services.PeerParser;

var parser = new PublicPeerParser();
var latCheck = new LatencyChecker() as ILatencyChecker;

var peers = parser.GetPeers().ToBlockingEnumerable().ToArray();

await latCheck.CheckAndFillLatency(peers.Where(peer => peer.Online).ToList());

foreach (var peer1 in peers
             .Where(peer => peer.Latency != null)
             .OrderBy(peer => peer.Reliability)
             .ThenBy(peer => peer.Latency))
{
    Console.WriteLine($"{peer1.Region} | {peer1.Latency} ms | {peer1.ReliabilityString} | {peer1.Uri}");
}

