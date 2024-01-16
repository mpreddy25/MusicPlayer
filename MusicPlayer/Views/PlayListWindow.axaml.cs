using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using LibVLCSharp.Shared;
using MusicPlayer.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace MusicPlayer.Views;

public partial class PlayListWindow : Window
{
    public PlayListWindow()
    {
        InitializeComponent();

        //var libvlc = new LibVLC();
        //var mediaPlayer = new MediaPlayer(libvlc);

        //string tempFilePath = String.Empty;// Path.Combine(Path.GetTempPath(), "temp_song.mp3");

        //var directory = Path.Combine(AppContext.BaseDirectory, "Songs");
        //if (Directory.Exists(directory))
        //{
        //    var songFiles = Directory.GetFiles(directory, "*.mp3");
        //    //_songs.AddRange(songFiles);
        //    tempFilePath = songFiles.FirstOrDefault();
        //}

        //Uri uri = new Uri(tempFilePath);
        //Media media = new Media(libvlc, uri);

        //mediaPlayer.Play(media);
    }
}