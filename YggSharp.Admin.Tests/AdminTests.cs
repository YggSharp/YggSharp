namespace YggSharp.Admin.Tests;

public class AdminTests
{
    private YggSharpAdminClient _client = null!;
    
    [OneTimeSetUp]
    public async Task Setup()
    {
        _client = new YggSharpAdminClient("127.0.0.1", 9001);
        var connectResult = await _client.Connect(default);

        if (!connectResult)
        {
            throw new InvalidOperationException("Yggdrasil admin isn't listening on 127.0.0.1:9001");
        }
    }

    [Test]
    public async Task GetSelf_HasValidResponse()
    {
        var rcpResponse = await _client.GetSelf(default);
        
        Assert.NotNull(rcpResponse);
        Assert.IsTrue(rcpResponse!.IsSuccessful);

        var response = rcpResponse.Response;
        
        Assert.IsNotEmpty(response.BuildName);
        Assert.IsNotEmpty(response.BuildVersion);
        Assert.IsNotEmpty(response.Subnet);
    }
}