using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests.GetSessions;

#nullable disable

public class YggAdminGetSessionsResponse
{
    [JsonPropertyName("sessions")]
    public List<YggAdminGetSessionEntry> Sessions { get; set; }
}