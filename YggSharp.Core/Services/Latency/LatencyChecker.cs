using System.Diagnostics;
using System.Net.Sockets;
using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.Services.Latency;

public class LatencyChecker : ILatencyChecker
{
    private const int DegreeOfParallelism = 16;
    private const int PeerTimeout = 1000;
    
    public Task CheckAndFillLatency(IEnumerable<Peer> peers)
    {
        peers.AsParallel()
            .WithDegreeOfParallelism(DegreeOfParallelism)
            .ForAll(CheckPeer);
        
        return Task.CompletedTask;
        
    }

    private static void CheckPeer(Peer peer)
    {
        try
        {
            using var tcp = new TcpClient();
            
            var clock = Stopwatch.StartNew();
        
            tcp.ConnectAsync(peer.Uri.Host, peer.Uri.Port).Wait(PeerTimeout);

            peer.Latency = clock.ElapsedMilliseconds;
        
            clock.Stop();
            tcp.Close();
        }
        catch
        {
            // ignored
        }
    }
}