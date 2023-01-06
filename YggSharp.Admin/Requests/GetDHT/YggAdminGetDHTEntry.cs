using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests.GetDHT;

public class YggAdminGetDHTEntry : YggAdminNodeInfo
{
    [JsonPropertyName("port")]
    public ulong Port { get; set; }

    [JsonPropertyName("rest")]
    public ulong Rest { get; set; }
}