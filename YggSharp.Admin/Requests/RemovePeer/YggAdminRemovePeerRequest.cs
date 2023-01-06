namespace YggSharp.Admin.Requests.RemovePeer;

public class YggAdminRemovePeerRequest : YggAdminRequest
{
    private readonly string _uri;
    private readonly string? _iface;

    public YggAdminRemovePeerRequest(string uri, string? iface)
    {
        _uri = uri;
        _iface = iface;
    }

    public override string RequestName => "removePeer";
    
    protected override IEnumerable<YggAdminArgument> GetArguments()
    {
        yield return YggAdminArgument.From("uri", _uri);

        if (_iface != null)
        {
            yield return YggAdminArgument.From("interface", _iface);
        }
    }
}
