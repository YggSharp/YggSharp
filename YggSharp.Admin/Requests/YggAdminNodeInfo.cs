using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests;

#nullable disable

public class YggAdminNodeInfo
{
    [JsonPropertyName("key")]
    public string PublicKey { get; set; }

    [JsonPropertyName("address")]
    public string IpAddress { get; set; }
}