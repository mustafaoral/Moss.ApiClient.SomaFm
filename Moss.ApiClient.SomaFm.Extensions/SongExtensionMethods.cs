namespace Moss.ApiClient.SomaFm.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="RecentlyPlayedSong"/>
    /// </summary>
    public static class SongExtensionMethods
    {
        /// <summary>
        /// Formats song details
        /// </summary>
        /// <param name="song">Song</param>
        public static string ToFormattedString(this RecentlyPlayedSong song)
        {
            return $"SomaFM | {song.ChannelId} | {song.Timestamp:yyyy-MM-ddTHH:mm:ssZ} | {song.Artist} | {song.Album ?? "NO_ALBUM"} | {song.Title}";
        }
    }
}
