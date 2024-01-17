using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Windows.Input;
using ReactiveUI;
using DynamicData;
using LibVLCSharp.Shared;
using Avalonia.Platform;

namespace MusicPlayer.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private LibVLC libvlc;
        private MediaPlayer _mediaPlayer;
        private ObservableCollection<string> _songs;
        private int _currentSongIndex;
        private bool _isPlaying;
        private string _selectedAsset;
        //private readonly ObservableCollection<AssetViewModel> _assets;

        //[ObservableProperty] private AssetViewModel? _selectedAsset;

        public ObservableCollection<string> Songs
        {
            get => _songs;
            set => this.RaiseAndSetIfChanged(ref _songs, value);
        }
        public string SelectedAsset
        {
            get => _selectedAsset;
            set => this.RaiseAndSetIfChanged(ref _selectedAsset, value);
        }

        public bool IsPlaying
        {
            get => _isPlaying;
            set => this.RaiseAndSetIfChanged(ref _isPlaying, value);
        }


        public ICommand TogglePlayPauseCommand { get; }
        public ICommand NextCommand { get; }

        public PlayerViewModel()
        {
            libvlc = new LibVLC();
            _mediaPlayer = new MediaPlayer(libvlc);
            _mediaPlayer.EndReached += OnMediaEnded;
            _songs = new ObservableCollection<string>();

            // Get the path to the app's data directory on Android
            string directory;

            directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Songs");

            // Get the names of embedded resources (assuming they are in the root namespace)
            var resourceNames = typeof(PlayerViewModel).Assembly.GetManifestResourceNames();

            // Filter the resource names to include only those in the "Songs" folder
            var songsResourceNames = resourceNames
                .Where(resourceName => resourceName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Now, check for MP3 files and add them to the ObservableCollection
            var songs = GetSongsFromResources(songsResourceNames);
            _songs.AddRange(songs);

            TogglePlayPauseCommand = ReactiveCommand.Create(TogglePlayPause);
            NextCommand = ReactiveCommand.Create(NextSong);

            var assets = AssetLoader
            .GetAssets(new Uri("avares://MusicPlayer/Assets"), new Uri("avares://MusicPlayer/"))
            .Where(x => x.AbsolutePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            .Select(x => x.AbsoluteUri);

            //_assets = new ObservableCollection<AssetViewModel>(assets);

            _selectedAsset = assets.FirstOrDefault();
        }

        private void OnMediaEnded(object sender, EventArgs e)
        {
            // When the media playback reaches the end, automatically play the next song
            NextSong();
        }

        private IEnumerable<string> GetSongsFromResources(List<string> resourceNames)
        {
            foreach (var resourceName in resourceNames)
            {
                using (var stream = typeof(PlayerViewModel).Assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        // Convert the stream to a temporary file path or load it directly, depending on your needs
                        // Example: Save the stream to a temporary file and add the file path to the ObservableCollection
                        var tempFilePath = Path.GetTempFileName();
                        using (var fileStream = File.OpenWrite(tempFilePath))
                        {
                            stream.CopyTo(fileStream);
                        }
                        yield return tempFilePath;

                        // Alternatively, load the stream directly (might require additional handling based on your audio player library)
                        // Example: Use a library that can play audio directly from a stream
                        // _songs.Add(stream);
                    }
                }
            }
        }

        public string CurrentSong => _songs.ElementAtOrDefault(_currentSongIndex);

        public void TogglePlayPause()
        {
            if (string.IsNullOrEmpty(CurrentSong))
                return;

            if (IsPlaying)
            {
                Pause();
            }
            else
            {
                if(_mediaPlayer.Length == -1)
                {
                    Play();
                }
                if (_mediaPlayer.Length == _mediaPlayer.Time)
                {
                    // If at the end of the song, move to the next song
                    NextSong();
                }
                else
                {
                    // If not at the end, just resume
                    Play();
                }
            }
            //IsPlaying = !IsPlaying;
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
            IsPlaying = false;
        }

        private void Play()
        {
            if (string.IsNullOrEmpty(CurrentSong))
                return;

            Media media = new Media(libvlc, new Uri(CurrentSong));
            _mediaPlayer.Play(media);
            IsPlaying = true;
        }

        public void NextSong()
        {
            if (_songs.Any())
            {
                _currentSongIndex = (_currentSongIndex + 1) % _songs.Count;
                Play();
            }
        }
    }
}

