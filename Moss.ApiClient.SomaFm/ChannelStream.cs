namespace Moss.ApiClient.SomaFm;

/// <summary>
/// Represents a channel stream in a format and quality
/// </summary>
/// <param name="Uri">URI</param>
/// <param name="Format">Format</param>
/// <param name="Quality">Quality</param>
public record ChannelStream(Uri Uri, StreamFormat Format, StreamQuality Quality);
