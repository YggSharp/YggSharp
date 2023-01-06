namespace YggSharp.Admin.Requests;

public record YggAdminArgument(string Key, string Value)
{
    public static YggAdminArgument From(string key, string value)
    {
        return new YggAdminArgument(key, value);
    }
}
