namespace YggSharp.Admin.Requests.GetSessions;

public class YggAdminGetSessionsRequest : YggAdminRequest
{
    public override string RequestName => "getSessions";
    
    protected override IEnumerable<YggAdminArgument> GetArguments()
    {
        return Enumerable.Empty<YggAdminArgument>();
    }
}