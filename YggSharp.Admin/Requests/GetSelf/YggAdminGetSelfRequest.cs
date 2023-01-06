namespace YggSharp.Admin.Requests.GetSelf;

public class YggAdminGetSelfRequest : YggAdminRequest
{
    public override string RequestName => "getSelf";
    
    protected override IEnumerable<YggAdminArgument> GetArguments()
    {
        return Enumerable.Empty<YggAdminArgument>();
    }
}