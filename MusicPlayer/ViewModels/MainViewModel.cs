using ReactiveUI;
using System.Windows.Input;

namespace MusicPlayer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand BuyMusicCommand { get; }

        public MainViewModel()
        {
            BuyMusicCommand = ReactiveCommand.Create(() =>
            {
                // Code here will be executed when the button is clicked.
            });
        }
    }
}
