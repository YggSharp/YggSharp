namespace YggSharp.Admin.Requests.GetDHT;

public class YggAdminGetDHTRequest : YggAdminRequest
{
    public override string RequestName => "getDHT";
    
    protected override IEnumerable<YggAdminArgument> GetArguments()
    {
        return Enumerable.Empty<YggAdminArgument>();
    }
}