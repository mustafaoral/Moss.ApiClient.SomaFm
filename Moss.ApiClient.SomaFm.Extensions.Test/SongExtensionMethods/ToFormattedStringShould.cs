using System;
using Xunit;

namespace Moss.ApiClient.SomaFm.Extensions.Test.SongExtensionMethods
{
    public class ToFormattedStringShould
    {
        [Fact]
        public void ReturnFormattedStringWhenAlbumIsProvided()
        {
            var timestamp = DateTimeOffset.UtcNow;
            var channelId = Guid.NewGuid().ToString();
            var artist = Guid.NewGuid().ToString();
            var album = Guid.NewGuid().ToString();
            var title = Guid.NewGuid().ToString();

            var song = new RecentlyPlayedSong(timestamp, channelId, artist, album, title);

            var result = song.ToFormattedString();

            Assert.Equal($"SomaFM | {song.ChannelId} | {song.Timestamp:yyyy-MM-ddTHH:mm:ssZ} | {song.Artist} | {song.Album} | {song.Title}", result);
        }

        [Fact]
        public void ReturnFormattedStringWhenAlbumIsNotProvided()
        {
            var timestamp = DateTimeOffset.UtcNow;
            var channelId = Guid.NewGuid().ToString();
            var artist = Guid.NewGuid().ToString();
            var title = Guid.NewGuid().ToString();

            var song = new RecentlyPlayedSong(timestamp, channelId, artist, null, title);

            var result = song.ToFormattedString();

            Assert.Equal($"SomaFM | {song.ChannelId} | {song.Timestamp:yyyy-MM-ddTHH:mm:ssZ} | {song.Artist} | NO_ALBUM | {song.Title}", result);
        }
    }
}
