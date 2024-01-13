using ReactiveUI;
using System.Reactive.Linq;
using System.Windows.Input;

namespace MusicPlayer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }
        public ICommand BuyMusicCommand { get; }

        public MainViewModel()
        {
            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

            BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();

                var result = await ShowDialog.Handle(store);
            });
        }
    }
}
