namespace Moss.ApiClient.SomaFm;

/// <summary>
/// Represents the response to retrieve recently played songs
/// </summary>
public record RetrieveRecentlyPlayedSongsResponse : Response
{
    /// <summary>
    /// Retrieved songs
    /// </summary>
    public RecentlyPlayedSong[] Songs { get; private set; }

    internal static RetrieveRecentlyPlayedSongsResponse CreateSuccess(RecentlyPlayedSong[] songs)
    {
        return new RetrieveRecentlyPlayedSongsResponse
        {
            Success = true,
            Songs = songs
        };
    }

    internal static RetrieveRecentlyPlayedSongsResponse CreateFailure(string message)
    {
        return new RetrieveRecentlyPlayedSongsResponse
        {
            ErrorMessage = message
        };
    }
}
