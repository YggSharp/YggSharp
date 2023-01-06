namespace YggSharp.Admin.Requests.GetPeers;

public class YggAdminPeersRequest : YggAdminRequest
{
    public override string RequestName => "GetPeers";

    protected override IEnumerable<YggAdminArgument> GetArguments()
    {
        return Enumerable.Empty<YggAdminArgument>();
    }
}
