namespace YggSharp.Admin.Requests.AddPeer;

public class YggAdminAddPeerRequest : YggAdminRequest
{
    private readonly string _uri;
    private readonly string? _iface;

    public YggAdminAddPeerRequest(string uri, string? iface)
    {
        _uri = uri;
        _iface = iface;
    }
    
    public override string RequestName => "addPeer";

    protected override IEnumerable<YggAdminArgument> GetArguments()
    {
        yield return YggAdminArgument.From("uri", _uri);

        if (_iface != null)
        {
            yield return YggAdminArgument.From("interface", _iface);
        }
    }
}