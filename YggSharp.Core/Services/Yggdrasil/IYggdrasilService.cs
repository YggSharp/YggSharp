namespace YggSharp.Core.Services.Yggdrasil;

public interface IYggdrasilService
{
    YggdrasilServiceStatus GetYggdrasilStatus();
    Task<bool> IsUpdateRequired(CancellationToken ct);
    Task<bool> InstallLatestRelease(CancellationToken ct);
}