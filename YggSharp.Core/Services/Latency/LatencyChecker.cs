using System.Diagnostics;
using System.Net.Sockets;
using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.Services.Latency;

public class LatencyChecker : ILatencyChecker
{
    private const int PeerTimeout = 1000;
    
    public async Task CheckAndFillLatency(IEnumerable<Peer> peers, int parallelism, int passes)
    {
        var tasks = peers.AsParallel()
            .WithDegreeOfParallelism(parallelism)
            .Select(async peer => await CheckPeer(peer, passes));

        await Task.WhenAll(tasks.ToArray());
    }
    
    private static async Task CheckPeer(Peer peer, int passes)
    {
        try
        {
            var latencies = new List<long>();
            
            for (var i = 0; i < passes; i++)
            {
                using var tcp = new TcpClient
                {
                    ReceiveTimeout = 1000,
                    SendTimeout = 1000
                };
            
                var clock = Stopwatch.StartNew();
        
                tcp.ConnectAsync(peer.Uri.Host, peer.Uri.Port).Wait(PeerTimeout);
                
                clock.Stop();
    
                if (tcp.Connected)
                {
                    latencies.Add(clock.ElapsedMilliseconds);
                }
                
                tcp.Close();
                
                
                if(i < passes - 1)
                {
                    await Task.Delay(1000);
                }
            }
            
            if(latencies.Count <= 0)
                return;

            peer.Latency ??= new PeerLatency
            {
                Avg = (long) latencies.Average(),
                Min = latencies.Min(),
                Max = latencies.Max(),
                Losses = latencies.Count < passes
            };
        }
        catch
        {
            // ignored
        }
    }
}