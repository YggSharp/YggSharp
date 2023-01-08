namespace YggSharp.Core.Models.Peer;

public class PeerLatency : IComparable<PeerLatency>
{
    public long Min { get; set; }
    public long Max { get; set; }
    public long Avg { get; set; }
    public bool Losses { get; set; }

    public override string ToString() => Avg.ToString();

    public int CompareTo(PeerLatency? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }
        
        return ReferenceEquals(null, other) ? 1 : Avg.CompareTo(other.Avg);
    }
}