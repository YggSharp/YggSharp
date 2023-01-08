using System.Text.Json.Serialization;

namespace YggSharp.Core.Models.Peer;

public class Peer : IEquatable<Peer>
{
    public Uri Uri { get; set; }
    public string Version { get; set; }
    public bool Online { get; set; }
    public Reliability Reliability { get; set; }
    public string Region { get; set; }

    [JsonIgnore]
    public PeerLatency? Latency { get; set; }
    
    public string ReliabilityString
    {
        get => Reliability.ToString().ToLowerInvariant();

        set
        {
            Reliability = value switch
            {
                "reliable" => Reliability.Reliable,
                "unreliable" => Reliability.Unreliable,
                "average" => Reliability.Average,
                "very unreliable" => Reliability.VeryUnreliable,
                "veryunreliable" => Reliability.VeryUnreliable,
                _ => Reliability.Other
            };
        }
    }

    public bool Equals(Peer? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Uri.Equals(other.Uri);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((Peer)obj);
    }

    public override int GetHashCode()
    {
        return Uri.GetHashCode();
    }
}