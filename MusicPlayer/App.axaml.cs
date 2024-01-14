using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MusicPlayer.ViewModels;
using MusicPlayer.Views;

namespace MusicPlayer
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new PlayListWindow
                {
                    DataContext = new PlayListWindowViewModel()
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new PlayListWindow
                {
                    DataContext = new PlayListWindowViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}