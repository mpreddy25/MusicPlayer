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

namespace MusicPlayer.ViewModels
{
    public class PlayListWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Song> Songs { get; set; } = new ObservableCollection<Song>();

        private Song _currentSong;
        public Song CurrentSong
        {
            get => _currentSong;
            set
            {
                if (_currentSong != value)
                {
                    this.RaiseAndSetIfChanged(ref _currentSong, value);
                   // _currentSong = value;
                    //this.RaisePropertyChanged();
                }
            }
        }

        //private MediaPlayer _mediaPlayer;

        //public ICommand PreviousCommand => new DelegateCommand(Previous);
        //public ICommand PlayCommand => new DelegateCommand(Play);
        //public ICommand PauseCommand => new DelegateCommand(Pause);
        //public ICommand NextCommand => new DelegateCommand(Next);
        //public ICommand ShowSongsListCommand => new DelegateCommand(ShowSongsList);

        public PlayListWindowViewModel()
        {
            // Load songs from embedded resources
            //Songs = LoadSongsFromEmbeddedResources();

            // Initialize media player
            //_mediaPlayer = new MediaPlayer();
        }

        // ... implementation of playback commands and song list handling ...
    }
}

