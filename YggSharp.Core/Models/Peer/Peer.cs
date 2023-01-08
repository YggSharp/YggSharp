namespace YggSharp.Core.Models.Peer;

public class Peer
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
        get
        {
            return Reliability.ToString().ToLowerInvariant();
        }

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
}