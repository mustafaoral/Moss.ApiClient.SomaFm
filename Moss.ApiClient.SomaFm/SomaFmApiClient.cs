using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Moss.ApiClient.SomaFm.Dto;

namespace Moss.ApiClient.SomaFm;

/// <inheritdoc/>
public sealed class SomaFmApiClient : ISomaFmApiClient
{
    internal class ApiResponse<T>
    {
        internal bool Success { get; set; }
        internal string Message { get; set; }
        internal T Dto { get; set; }
    }

    private static readonly Lazy<HttpClient> _httpClient = new(() =>
    {
        var client = new HttpClient();

        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

        return client;
    });

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    /// <inheritdoc/>
    public Task<RetrieveChannelsResponse> RetrieveChannels(CancellationToken cancellationToken)
    {
        return RetrieveChannels(_httpClient.Value, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<RetrieveChannelsResponse> RetrieveChannels(HttpClient httpClient, CancellationToken cancellationToken)
    {
        var response = await RetrieveChannelsInternal(httpClient, cancellationToken).ConfigureAwait(false);

        if (!response.Success)
        {
            return RetrieveChannelsResponse.CreateFailure(response.Message);
        }

        var mappedEntities = response.Dto.Channels.Select(dto => new Channel(
            Id: dto.Id,
            Title: dto.Title,
            Description: dto.Description,
            Dj: dto.Dj,
            DjEmail: dto.Djmail,
            Genre: dto.Genre,
            Images: new Dictionary<ImageSize, Uri>
            {
                [ImageSize.Regular] = dto.Image,
                [ImageSize.Large] = dto.LargeImage,
                [ImageSize.ExtraLarge] = dto.XlImage,
            },
            TimestampUpdated: DateTimeOffset.UnixEpoch.AddSeconds(dto.Updated),
            Streams: dto.Playlists.Select(x => new ChannelStream(x.Url, Enum.Parse<StreamFormat>(x.Format, ignoreCase: true), Enum.Parse<StreamQuality>(x.Quality, ignoreCase: true))).ToArray(),
            PreRoll: dto.PreRoll,
            Listeners: dto.Listeners,
            LastPlaying: dto.LastPlaying)).ToArray();

        return RetrieveChannelsResponse.CreateSuccess(mappedEntities);
    }

    private Task<ApiResponse<ChannelsDto>> RetrieveChannelsInternal(HttpClient httpClient, CancellationToken cancellationToken)
    {
        return Get<ChannelsDto>(httpClient, "https://api.somafm.com/channels.json", cancellationToken);
    }

    /// <inheritdoc/>
    public Task<RetrieveRecentlyPlayedSongsResponse> RetrieveRecentlyPlayedSongs(string channelId, CancellationToken cancellationToken)
    {
        return RetrieveRecentlyPlayedSongs(_httpClient.Value, channelId, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<RetrieveRecentlyPlayedSongsResponse> RetrieveRecentlyPlayedSongs(ChannelId channelId, CancellationToken cancellationToken)
    {
        return RetrieveRecentlyPlayedSongs(channelId.Value, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<RetrieveRecentlyPlayedSongsResponse> RetrieveRecentlyPlayedSongs(HttpClient httpClient, string channelId, CancellationToken cancellationToken)
    {
        var response = await RetrieveRecentlyPlayedSongsInternal(httpClient, $"https://api.somafm.com/songs/{channelId}.json", cancellationToken).ConfigureAwait(false);

        if (!response.Success)
        {
            return RetrieveRecentlyPlayedSongsResponse.CreateFailure(response.Message);
        }

        var mappedEntities = response.Dto.Songs.Select(x => new RecentlyPlayedSong(
            Timestamp: DateTimeOffset.UnixEpoch.AddSeconds(x.Date),
            ChannelId: channelId,
            Artist: x.Artist,
            Album: string.IsNullOrWhiteSpace(x.Album) ? null : x.Album,
            Title: x.Title)).ToArray();

        return RetrieveRecentlyPlayedSongsResponse.CreateSuccess(mappedEntities);
    }

    /// <inheritdoc/>
    public Task<RetrieveRecentlyPlayedSongsResponse> RetrieveRecentlyPlayedSongs(HttpClient httpClient, ChannelId channelId, CancellationToken cancellationToken)
    {
        return RetrieveRecentlyPlayedSongs(httpClient, channelId.Value, cancellationToken);
    }

    private Task<ApiResponse<SongsDto>> RetrieveRecentlyPlayedSongsInternal(HttpClient httpClient, string uri, CancellationToken cancellationToken)
    {
        return Get<SongsDto>(httpClient, uri, cancellationToken);
    }

    internal async Task<ApiResponse<T>> Get<T>(HttpClient httpClient, string uri, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        if (!httpClient.DefaultRequestHeaders.AcceptEncoding.Any(x => "gzip".Equals(x.Value, StringComparison.InvariantCultureIgnoreCase)))
        {
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        }

        using (var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
        {
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<T>
                {
                    Message = $"Server returned {(int)response.StatusCode} {response.StatusCode}"
                };
            }

            if (!response.Content.Headers.ContentEncoding.Contains("gzip", StringComparer.InvariantCultureIgnoreCase))
            {
                using (var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                {
                    return new ApiResponse<T>
                    {
                        Success = true,
                        Dto = await JsonSerializer.DeserializeAsync<T>(stream, _jsonSerializerOptions, cancellationToken).ConfigureAwait(false)
                    };
                }
            }

            using (var inputStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
            using (var outputStream = new MemoryStream())
            using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                compressionStream.CopyTo(outputStream);

                inputStream.Flush();

                outputStream.Seek(0, SeekOrigin.Begin);

                return new ApiResponse<T>
                {
                    Success = true,
                    Dto = await JsonSerializer.DeserializeAsync<T>(outputStream, _jsonSerializerOptions, cancellationToken).ConfigureAwait(false)
                };
            }
        }
    }
}
