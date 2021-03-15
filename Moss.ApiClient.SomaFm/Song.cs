using System;

namespace Moss.ApiClient.SomaFm
{
    public class Song
    {
        public DateTime PlayedWhenUtc { get; }
        public string Artist { get; }
        public string Album { get; }
        public string Title { get; }

        public Song(DateTime playedWhenUtc, string artist, string album, string title)
        {
            PlayedWhenUtc = playedWhenUtc;
            Artist = artist;
            Album = album;
            Title = title;
        }
    }
}
