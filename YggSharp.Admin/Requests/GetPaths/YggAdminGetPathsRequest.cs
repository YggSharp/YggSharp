namespace YggSharp.Admin.Requests.GetPaths;

public class YggAdminGetPathsRequest : YggAdminRequest
{
    public override string RequestName => "getPaths";
    
    protected override IEnumerable<YggAdminArgument> GetArguments()
    {
        return Enumerable.Empty<YggAdminArgument>();
    }
}