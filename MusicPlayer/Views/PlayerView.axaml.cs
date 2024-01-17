using Avalonia.Controls;
using Avalonia.Skia.Lottie;
using SkiaSharp.Skottie;

namespace MusicPlayer.Views;

public partial class PlayerView : UserControl
{
    private readonly Lottie _playPauseIcon;
    private readonly Lottie _musicAnimation;
    public PlayerView()
    {
        InitializeComponent();
        DataContextChanged += YourView_DataContextChanged;

        // Initialize the Lottie animation view
        _playPauseIcon = this.FindControl<Lottie>("playPauseIcon");
        _musicAnimation = this.FindControl<Lottie>("musicAnimation");
    }

    private void YourView_DataContextChanged(object sender, System.EventArgs e)
    {
        if (DataContext is MusicPlayer.ViewModels.PlayerViewModel viewModel)
        {
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
    }

    private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MusicPlayer.ViewModels.PlayerViewModel.IsPlaying))
        {
            // Update the Path property of the play/pause icon based on the IsPlaying state
            UpdatePlayPauseIconPath(((MusicPlayer.ViewModels.PlayerViewModel)DataContext).IsPlaying);
        }
    }

    private void UpdatePlayPauseIconPath(bool isPlaying)
    {
        // Set the Path property of the play/pause icon based on the IsPlaying state
        _playPauseIcon.Path = isPlaying ? "avares://MusicPlayer/Assets/Pause_Animation.json" : "avares://MusicPlayer/Assets/PlayAnimation.json";
        if(isPlaying)
        {
            _playPauseIcon.Width = 100;
            _playPauseIcon.Margin = new Avalonia.Thickness(20);
            _musicAnimation.RepeatCount = -1;
        }
        else
        {
            _playPauseIcon.Width = 150;
            _playPauseIcon.Margin = new Avalonia.Thickness(0);
            _musicAnimation.RepeatCount = 1;
        }
    }
}
