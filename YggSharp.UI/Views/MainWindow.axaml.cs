using Avalonia.Controls;
using Avalonia.Input;

namespace YggSharp.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if(!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            return;
        
        BeginMoveDrag(e);
    }
}