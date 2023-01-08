using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Splat;
using YggSharp.Admin;
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
        await CastedDataContext.Initialize();
    }
}