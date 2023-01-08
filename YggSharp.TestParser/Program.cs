using YggSharp.Core.LatencyCheckerService;
using YggSharp.Core.PeerParser;

var parser = new PublicPeerParser();
var latCheck = new LatencyChecker() as ILatencyChecker;
var peers = await parser.GetPeers();

await latCheck.CheckLatency(peers.Where(peer => peer.Online).ToList());

foreach (var peer1 in peers.Where(peer => peer.Latency != null))
{
    Console.WriteLine($"{peer1.Region} | Losses: {peer1.Latency!.Losses} | {peer1.Latency.Avg} ms | {peer1.ReliabilityString} | {peer1.Uri}");
}

Console.ReadLine();
