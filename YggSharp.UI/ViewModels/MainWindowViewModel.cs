using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using ReactiveUI;
using Splat;
using YggSharp.Admin;
using YggSharp.Core.Services.Yggdrasil;

namespace YggSharp.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private static readonly IYggdrasilService Yggdrasil = Locator.Current.GetService<IYggdrasilService>();
    private static readonly IYggSharpAdminClient YggSharpAdmin = Locator.Current.GetService<IYggSharpAdminClient>();
    
    #region Window Properties
    private string _loadingStatus = "status";
    public string LoadingStatus
    {
        get => _loadingStatus;
        set => this.RaiseAndSetIfChanged(ref _loadingStatus, value);
    }
    #endregion

    public async Task Initialize()
    {
        LoadingStatus = "Connecting to Yggdrasil Service socket";

        var socketConnected = await YggSharpAdmin.Connect(default);
        
        if(socketConnected)
        {
            LoadingStatus += " ... OK";
        }
        else
        {
            LoadingStatus = " ... FAILED";
        }
    }

}