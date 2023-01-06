using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace YggSharp.Admin.Requests;

public abstract class YggAdminRequest
{
    [JsonPropertyName("request")]
    public abstract string RequestName { get; }

    [JsonPropertyName("arguments")]
    public IReadOnlyDictionary<string, string> Arguments => TransformArguments();

    [JsonPropertyName("keepalive")]
    public bool KeepAlive { get; set; } = true;

    protected abstract IEnumerable<YggAdminArgument> GetArguments();

    private IReadOnlyDictionary<string, string> TransformArguments()
    {
        return GetArguments().ToImmutableDictionary(a => a.Key, a => a.Value);
    }
}
