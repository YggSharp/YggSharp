using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests.GetPaths;

#nullable disable

public class YggAdminGetPathsResponse
{
    [JsonPropertyName("paths")]
    public List<YggAdminGetPathsEntry> Paths { get; set; }
}