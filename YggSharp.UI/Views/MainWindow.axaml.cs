using System;
using System.Threading.Tasks;
using Avalonia.Input;
using Splat;
using YggSharp.Core.Services.Yggdrasil;
using YggSharp.UI.ViewModels;

namespace YggSharp.UI.Views;

public partial class MainWindow : WindowWithDataContext<MainWindowViewModel>
{
    private readonly IYggdrasilService _yggdrasilService = Locator.Current.GetService<IYggdrasilService>();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            return;

        BeginMoveDrag(e);
    }

    private async void Window_OnOpened(object? sender, EventArgs e)
    {
        Task.Run(CastedDataContext.Initialize);
    }
}