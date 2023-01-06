using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests.GetSessions;

public class YggAdminGetSessionEntry : YggAdminNodeInfo
{
    [JsonPropertyName("bytes_recvd")]
    public ulong BytesRecvd { get; set; }
    
    [JsonPropertyName("bytes_sent")]
    public ulong BytesSent { get; set; }
    
    [JsonPropertyName("uptime")]
    public float Uptime { get; set; }
}