using Avalonia.Controls;
using Avalonia.ReactiveUI;
using MusicPlayer.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace MusicPlayer.Views
{
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }

        private async Task DoShowDialogAsync(InteractionContext<MusicStoreViewModel,
                                                AlbumViewModel?> interaction)
        {
            var dialog = new MusicStoreWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<AlbumViewModel?>(this);
            interaction.SetOutput(result);
        }
    }
}