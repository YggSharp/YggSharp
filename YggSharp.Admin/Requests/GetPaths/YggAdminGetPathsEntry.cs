using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests.GetPaths;

#nullable disable

public class YggAdminGetPathsEntry : YggAdminNodeInfo
{
    [JsonPropertyName("path")]
    public ulong[] Path { get; set; }
}