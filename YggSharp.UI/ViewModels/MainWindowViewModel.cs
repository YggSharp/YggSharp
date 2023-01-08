using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;
using Splat;
using YggSharp.Admin;
using YggSharp.Core.Services.Yggdrasil;

namespace YggSharp.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IYggdrasilService _yggdrasil = Locator.Current.GetService<IYggdrasilService>();
    private readonly IYggSharpAdminClient _yggSharpAdmin = Locator.Current.GetService<IYggSharpAdminClient>();
    
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
        var socketConnected = await _yggSharpAdmin.Connect(default);
        
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