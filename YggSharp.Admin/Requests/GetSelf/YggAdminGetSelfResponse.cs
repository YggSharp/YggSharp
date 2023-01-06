using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests.GetSelf;

#nullable disable

#nullable disable

public class YggAdminGetSelfResponse : YggAdminNodeInfo
{
    [JsonPropertyName("build_name")]
    public string BuildName { get; set; }

    [JsonPropertyName("build_version")]
    public string BuildVersion { get; set; }

    [JsonPropertyName("coords")]
    public ulong[] Coords { get; set; }
    
    [JsonPropertyName("subnet")]
    public string Subnet { get; set; }
}
