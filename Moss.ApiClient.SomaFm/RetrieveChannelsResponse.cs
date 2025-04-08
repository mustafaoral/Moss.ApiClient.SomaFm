namespace Moss.ApiClient.SomaFm;

/// <summary>
/// Represents the response to retrieve channels
/// </summary>
public record RetrieveChannelsResponse : Response
{
    /// <summary>
    /// Retrieved channels
    /// </summary>
    public Channel[] Channels { get; private set; }

    internal static RetrieveChannelsResponse CreateSuccess(Channel[] channels)
    {
        return new RetrieveChannelsResponse
        {
            Success = true,
            Channels = channels
        };
    }

    internal static RetrieveChannelsResponse CreateFailure(string message)
    {
        return new RetrieveChannelsResponse
        {
            ErrorMessage = message
        };
    }
}
