namespace Moss.ApiClient.SomaFm;

/// <summary>
/// Represents a recently played song on a channel
/// </summary>
/// <param name="Timestamp">Timestamp of when the song started playing</param>
/// <param name="ChannelId">Channel ID</param>
/// <param name="Artist">Artist</param>
/// <param name="Album">Album</param>
/// <param name="Title">Title</param>
public record RecentlyPlayedSong(DateTimeOffset Timestamp, string ChannelId, string Artist, string Album, string Title);
