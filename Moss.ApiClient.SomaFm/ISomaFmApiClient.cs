namespace Moss.ApiClient.SomaFm;

/// <summary>
/// Provides means to retrieve channels are recently played songs
/// </summary>
public interface ISomaFmApiClient
{
    /// <summary>
    /// Retrieves channels
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An instance of <see cref="RetrieveChannelsResponse"/></returns>
    Task<RetrieveChannelsResponse> RetrieveChannels(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves channels
    /// </summary>
    /// <param name="httpClient"><see cref="HttpClient"/> to use</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An instance of <see cref="RetrieveChannelsResponse"/></returns>
    Task<RetrieveChannelsResponse> RetrieveChannels(HttpClient httpClient, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves channels
    /// </summary>
    /// <param name="channelId">Channel ID</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An instance of <see cref="RetrieveRecentlyPlayedSongsResponse"/></returns>
    Task<RetrieveRecentlyPlayedSongsResponse> RetrieveRecentlyPlayedSongs(string channelId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves channels
    /// </summary>
    /// <param name="channelId">Channel ID</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An instance of <see cref="RetrieveRecentlyPlayedSongsResponse"/></returns>
    Task<RetrieveRecentlyPlayedSongsResponse> RetrieveRecentlyPlayedSongs(ChannelId channelId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves channels
    /// </summary>
    /// <param name="httpClient"><see cref="HttpClient"/> to use</param>
    /// <param name="channelId">Channel ID</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An instance of <see cref="RetrieveRecentlyPlayedSongsResponse"/></returns>
    Task<RetrieveRecentlyPlayedSongsResponse> RetrieveRecentlyPlayedSongs(HttpClient httpClient, string channelId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves channels
    /// </summary>
    /// <param name="httpClient"><see cref="HttpClient"/> to use</param>
    /// <param name="channelId">Channel ID</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An instance of <see cref="RetrieveRecentlyPlayedSongsResponse"/></returns>
    Task<RetrieveRecentlyPlayedSongsResponse> RetrieveRecentlyPlayedSongs(HttpClient httpClient, ChannelId channelId, CancellationToken cancellationToken);
}
