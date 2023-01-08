using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Splat;
using YggSharp.Core.Services;
using YggSharp.UI.Misc;
using YggSharp.UI.ViewModels;
using YggSharp.UI.Views;

namespace YggSharp.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = SetupDI();
        var dependencyResolver = new MsDependencyResolver(services);
        
        Locator.SetLocator(dependencyResolver);
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Locator.Current.GetService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static IServiceCollection SetupDI()
    {
        var container = new ServiceCollection();

        container.AddSingleton(new MainWindow
        {
            DataContext = new MainWindowViewModel()
        });

        container.AddYggdrasilServices();
        
        return container;
    }
}