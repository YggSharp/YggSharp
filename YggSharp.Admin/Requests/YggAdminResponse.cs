using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests;

public class YggAdminResponse<TRequest, TResponse>
    where TRequest : YggAdminRequest
    where TResponse : class
{
    [JsonIgnore]
    public bool IsSuccessful => Status.Equals("success", StringComparison.InvariantCultureIgnoreCase);
    
    [JsonPropertyName("status")]
    public string Status { get; set; } = null!;

    [JsonPropertyName("error")]
    public string Error { get; set; } = null!;

    [JsonPropertyName("request")]
    public TRequest? Request { get; set; }

    [JsonPropertyName("response")]
    public TResponse Response { get; set; } = null!;
}
