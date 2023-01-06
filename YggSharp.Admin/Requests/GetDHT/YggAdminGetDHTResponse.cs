using System.Text.Json.Serialization;

#nullable disable

namespace YggSharp.Admin.Requests.GetDHT;

public class YggAdminGetDHTResponse
{
    [JsonPropertyName("dht")]
    public List<YggAdminGetDHTEntry> Entries { get; set; }
}