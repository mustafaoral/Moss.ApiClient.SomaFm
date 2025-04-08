namespace Moss.ApiClient.SomaFm;

/// <summary>
/// Represents a channel
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Title">Title</param>
/// <param name="Description">Description</param>
/// <param name="Dj">DJ</param>
/// <param name="DjEmail">DJ email</param>
/// <param name="Genre">Genre</param>
/// <param name="Images">Dictionary of channel images</param>
/// <param name="TimestampUpdated">Last updated</param>
/// <param name="Streams">Collection of streams by format and quality</param>
/// <param name="PreRoll">List of URIs to announcements between songs specific to the channel</param>
/// <param name="Listeners">Number of listeners</param>
/// <param name="LastPlaying">Last song played (or playing)</param>
public record Channel(
    string Id,
    string Title,
    string Description,
    string Dj,
    string DjEmail,
    string Genre,
    Dictionary<ImageSize, Uri> Images,
    DateTimeOffset TimestampUpdated,
    ChannelStream[] Streams,
    Uri[] PreRoll,
    uint Listeners,
    string LastPlaying);
