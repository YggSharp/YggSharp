using System.Text.Json;
using System.Text.Json.Serialization;
using YggSharp.Core.LatencyCheckerService;
using YggSharp.Core.Models.Peer;
using YggSharp.Core.PeerParser;

var parser = new PublicPeerParser();
var latCheck = new LatencyChecker();

var peers = await parser.GetPeers();

await latCheck.CheckLatency(peers.Where(peer => peer.Online).ToList());

foreach (var peer1 in peers)
{
    Console.WriteLine($"{peer1.Region} | {peer1.Latency} ms | {peer1.ReliabilityString} | {peer1.Uri}");
}

