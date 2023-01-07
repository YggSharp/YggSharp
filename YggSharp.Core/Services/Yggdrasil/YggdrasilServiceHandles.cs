using PInvoke;

namespace YggSharp.Core.Services.Yggdrasil;

public class YggdrasilServiceHandles : IDisposable
{
    public AdvApi32.SafeServiceHandle ScManagerHandle { get; }
    public AdvApi32.SafeServiceHandle ServiceHandle { get; }

    public bool ServiceHandleValid => !ServiceHandle.IsInvalid;

    public YggdrasilServiceHandles(AdvApi32.SafeServiceHandle scManagerHandle, AdvApi32.SafeServiceHandle serviceHandle)
    {
        ScManagerHandle = scManagerHandle;
        ServiceHandle = serviceHandle;
    }

    public void Dispose()
    {
        ScManagerHandle.Dispose();
        ServiceHandle.Dispose();
    }
}