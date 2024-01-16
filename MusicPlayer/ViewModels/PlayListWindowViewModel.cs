using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Joins;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Avalonia;
using Avalonia.Media;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using ReactiveUI;
using DynamicData;
using LibVLCSharp.Shared;
using Avalonia.Platform;

namespace MusicPlayer.ViewModels
{
    public class PlayListWindowViewModel : ViewModelBase
    {
        private LibVLC libvlc;
        private MediaPlayer _mediaPlayer;
        private ObservableCollection<string> _songs;
        private int _currentSongIndex;

        public ObservableCollection<string> Songs
        {
            get => _songs;
            set => this.RaiseAndSetIfChanged(ref _songs, value);
        }

        //private MediaPlayer _mediaPlayer;

        public ICommand PreviousCommand { get; }
        public ICommand PlayCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand ShowSongsListCommand { get; }

        public PlayListWindowViewModel()
        {
            libvlc = new LibVLC();
            _mediaPlayer = new MediaPlayer(libvlc);

            _songs = new ObservableCollection<string>();

            // Get the path to the app's data directory on Android
            string directory;

            directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Songs");
            //// Check if running on Android
            //if (AvaloniaLocator.Current.GetService<RuntimePlatform>() == RuntimePlatform.Android)
            //{
            //    directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Songs");
            //}
            //else
            //{
            //    // For other platforms, use the original path
            //    directory = Path.Combine(AppContext.BaseDirectory, "Songs");
            //}

            // Get the names of embedded resources (assuming they are in the root namespace)
            var resourceNames = typeof(PlayListWindowViewModel).Assembly.GetManifestResourceNames();

            // Filter the resource names to include only those in the "Songs" folder
            var songsResourceNames = resourceNames
                .Where(resourceName => resourceName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Now, check for MP3 files and add them to the ObservableCollection
            var songs = GetSongsFromResources(songsResourceNames);
            _songs.AddRange(songs);

            PlayCommand = ReactiveCommand.Create(Play);
            PauseCommand = ReactiveCommand.Create(Pause);
            //PreviousCommand = ReactiveCommand.Create(Previous);
            NextCommand = ReactiveCommand.Create(Next);
            //PlayCommand = ReactiveCommand.Create(Play);
        }

        private IEnumerable<string> GetSongsFromResources(List<string> resourceNames)
        {
            foreach (var resourceName in resourceNames)
            {
                using (var stream = typeof(PlayListWindowViewModel).Assembly.GetManifestResourceStream(resourceName))
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

        public void Play()
        {
            if (string.IsNullOrEmpty(CurrentSong))
                return;

            Media media = new Media(libvlc, new Uri(CurrentSong));
            _mediaPlayer.Play(media);
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
        }

        public void Next()
        {
            _currentSongIndex = (_currentSongIndex + 1) % _songs.Count;
            Play();
        }
    }
}

