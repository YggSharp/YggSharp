using CliWrap;
using Octokit;
using PInvoke;
using YggSharp.Admin;

namespace YggSharp.Core.Services.Yggdrasil;

public class YggdrasilService : IYggdrasilService
{
    private const string RepoOwner = "yggdrasil-network";
    private const string RepoName = "yggdrasil-go";
    
    private readonly IYggSharpAdminClient _adminClient;

    public YggdrasilService(IYggSharpAdminClient adminClient)
    {
        _adminClient = adminClient;
    }

    private readonly Lazy<HttpClient> _httpClient = new(() => new HttpClient(new HttpClientHandler
    {
        AllowAutoRedirect = true
    })
    {
        DefaultRequestHeaders =
        {
            {"User-Agent", "YggSharp/beta"}
        }
    });
    
    private readonly Lazy<GitHubClient> _githubClient = new(() => new GitHubClient(new ProductHeaderValue("YggSharp", "beta")));

    public YggdrasilServiceStatus GetYggdrasilStatus()
    {
        using var serviceHandles = OpenYggdrasilService();

        if (serviceHandles == null)
        {
            return YggdrasilServiceStatus.UnknownError;
        }
        
        if (!serviceHandles.ServiceHandleValid)
        {
            var lastError = Kernel32.GetLastError();

            if (lastError == Win32ErrorCode.ERROR_SERVICE_DOES_NOT_EXIST)
            {
                return YggdrasilServiceStatus.NotInstalled;
            }

            // TODO: log
            return YggdrasilServiceStatus.UnknownError;
        }

        var serviceStatus = new AdvApi32.SERVICE_STATUS();
        
        if (!AdvApi32.QueryServiceStatus(serviceHandles.ServiceHandle, ref serviceStatus))
        {
            // TODO: log
            return YggdrasilServiceStatus.UnknownError;
        }

        return serviceStatus.dwCurrentState == AdvApi32.ServiceState.SERVICE_RUNNING ?
            YggdrasilServiceStatus.Running : YggdrasilServiceStatus.Stopped;
    }

    public async Task<bool> IsUpdateRequired(CancellationToken ct)
    {
        var nodeInfo = await _adminClient.GetSelf(ct);

        if (nodeInfo is not { IsSuccessful: true })
        {
            // TODO: log
            return false;
        }
        
        var currentVersion = nodeInfo.Response.BuildVersion;

        var latestReleaseTuple = await GetLatestRelease();

        if (latestReleaseTuple == null)
        {
            // TODO: log
            return false;
        }

        var (release, _) = latestReleaseTuple.Value;
        var latestVersion = release.TagName.Trim('v');

        return !latestVersion.Equals(currentVersion, StringComparison.InvariantCultureIgnoreCase);
    }
    
    private static YggdrasilServiceHandles? OpenYggdrasilService()
    {
        var genericReadMask = new Kernel32.ACCESS_MASK((uint)Kernel32.ACCESS_MASK.GenericRight.GENERIC_READ);
        var scManagerHandle = AdvApi32.OpenSCManager(null, null, genericReadMask);

        if (scManagerHandle == null || scManagerHandle.IsInvalid)
        {
            // TODO: log
            return null;
        }

        var serviceHandle = AdvApi32.OpenService(scManagerHandle, "Yggdrasil", genericReadMask);

        return new YggdrasilServiceHandles(scManagerHandle, serviceHandle);
    }

    private async Task<(Release, ReleaseAsset)?> GetLatestRelease()
    {
        try
        {
            var client = _githubClient.Value;
            var release = await client.Repository.Release.GetLatest(RepoOwner, RepoName);

            var installerOsType = Environment.Is64BitOperatingSystem ? "x64.msi" : "x86.msi";
            var installerAsset = release.Assets.SingleOrDefault(asset => asset.Name.Contains(installerOsType, StringComparison.InvariantCultureIgnoreCase));

            if (installerAsset == null)
            {
                // TODO: log
                return null;
            }
            
            return (release, installerAsset);
        }
        catch
        {
            // TODO: log
            return null;
        }
    }

    public async Task<bool> InstallLatestRelease(CancellationToken ct)
    {
        var latestAsset = await GetLatestRelease();

        if (latestAsset == null)
        {
            return false;
        }

        var installerPath = await DownloadRelease(latestAsset.Value.Item2, ct);

        if (installerPath == null)
        {
            return false;
        }

        var installResult = await new Command("msiexec")
            .WithArguments(new []{"/i", installerPath})
            .WithValidation(CommandResultValidation.None)
            .ExecuteAsync();

        return installResult.ExitCode == 0;
    }
    
    private async Task<string?> DownloadRelease(ReleaseAsset releaseAsset, CancellationToken ct)
    {
        var client = _httpClient.Value;

        var tempFilePath = Path.Combine(Path.GetTempPath(), $"ygg_installer_{Guid.NewGuid():N}.msi");
        var tempFile = File.Create(tempFilePath);

        var installerStream = await client.GetStreamAsync(releaseAsset.BrowserDownloadUrl, ct);
        await installerStream.CopyToAsync(tempFile, ct);

        await tempFile.FlushAsync(ct);
        tempFile.Close();
        
        return tempFilePath;
    }
}