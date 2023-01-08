namespace YggSharp.Core.Models.Peer;

public class PeerLatency
{
    public long Min { get; set; }
    public long Max { get; set; }
    public long Avg { get; set; }
    public bool Losses { get; set; }

    public override string ToString() => Avg.ToString();
}