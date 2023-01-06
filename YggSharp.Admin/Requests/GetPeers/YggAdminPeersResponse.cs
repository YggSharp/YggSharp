using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests.GetPeers;

#nullable disable

public class YggAdminPeersResponse
{
    [JsonPropertyName("peers")]
    public IList<YggAdminPeer> Peers { get; set; }
}