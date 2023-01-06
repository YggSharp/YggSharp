using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests.GetPeers;

#nullable disable

public class YggAdminPeer
{
    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("port")]
    public ulong Port { get; set; }

    [JsonPropertyName("priority")]
    public ulong Priority { get; set; }

    [JsonPropertyName("coords")]
    public List<ulong> Coords { get; set; }

    [JsonPropertyName("remote")]
    public string Remote { get; set; }

    [JsonPropertyName("bytes_recvd")]
    public ulong BytesRecvd { get; set; }

    [JsonPropertyName("bytes_sent")]
    public ulong BytesSent { get; set; }

    [JsonPropertyName("uptime")]
    public float Uptime { get; set; }
}
