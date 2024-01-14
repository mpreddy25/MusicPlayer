using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Models
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }
        public string FilePath { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }

        public Song(string filePath)
        {
            // Extract information from the filePath (e.g., filename, tags)
            this.FilePath = filePath;
            // ...
        }
    }
}
