using YggSharp.Admin;

var client = new YggSharpAdminClient("127.0.0.1", 9001);

if (!await client.Connect(default))
{
    Console.WriteLine("Failed to connect.");
    return;
}

var adminResponse = await client.GetSelf(default);

if (adminResponse is not { IsSuccessful: true })
{
    Console.WriteLine("Failed to execute 'GetSelf' method.");
    return;
}

var response = adminResponse.Response;

Console.WriteLine($"Build name: {response.BuildName}");
Console.WriteLine($"Build version: {response.BuildVersion}");
Console.WriteLine($"Subnet: {response.Subnet}");
