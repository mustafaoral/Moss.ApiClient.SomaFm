using System.Net;
using System.Text.Json;
using Moss.ApiClient.SomaFm.Dto;

namespace Moss.ApiClient.SomaFm.Test.SomaFmApiClient;

public class RetrieveChannelsShould
{
    [Fact]
    public async Task RetrieveChannels()
    {
        // arrange
        var requestUri = "https://api.somafm.com/channels.json";

        var dto = new ChannelDto(
            Id: Guid.NewGuid().ToString(),
            Title: Guid.NewGuid().ToString(),
            Description: Guid.NewGuid().ToString(),
            Dj: Guid.NewGuid().ToString(),
            Djmail: Guid.NewGuid().ToString(),
            Genre: Guid.NewGuid().ToString(),
            Image: CreateUri(),
            LargeImage: CreateUri(),
            XlImage: CreateUri(),
            Updated: 123,
            Playlists: new[] { new PlaylistDto(CreateUri(), "mp3", "low") },
            PreRoll: new[] { CreateUri() },
            Listeners: 123,
            LastPlaying: Guid.NewGuid().ToString());

        var channelsDto = new ChannelsDto(new[] { dto });

        var httpClient = new HttpClient(TestHelper.CreateHttpMessageHandlerMock(HttpStatusCode.OK, requestUri, new StringContent(JsonSerializer.Serialize(channelsDto))).Object);

        // act
        var result = await new SomaFm.SomaFmApiClient().RetrieveChannels(httpClient, CancellationToken.None);

        // assert
        Assert.NotNull(result);

        var channel = result.Channels.Single();

        Assert.Equal(dto.Id, channel.Id);
        Assert.Equal(dto.Title, channel.Title);
        Assert.Equal(dto.Description, channel.Description);
        Assert.Equal(dto.Dj, channel.Dj);
        Assert.Equal(dto.Djmail, channel.DjEmail);
        Assert.Equal(dto.Genre, channel.Genre);
        Assert.Equal(dto.Image, channel.Images[ImageSize.Regular]);
        Assert.Equal(dto.LargeImage, channel.Images[ImageSize.Large]);
        Assert.Equal(dto.XlImage, channel.Images[ImageSize.ExtraLarge]);
        Assert.Equal(DateTimeOffset.UnixEpoch.AddSeconds(dto.Updated), channel.TimestampUpdated);
        Assert.Equal(dto.Playlists.Single().Url, channel.Streams.Single().Uri);
        Assert.Equal(dto.Playlists.Single().Format, channel.Streams.Single().Format.ToString(), ignoreCase: true);
        Assert.Equal(dto.Playlists.Single().Quality, channel.Streams.Single().Quality.ToString(), ignoreCase: true);
        Assert.Equal(dto.PreRoll.Single(), channel.PreRoll.Single());
        Assert.Equal(dto.Listeners, channel.Listeners);
        Assert.Equal(dto.LastPlaying, channel.LastPlaying);
    }

    [Fact]
    public async Task ReturnFailureResponseWhenApiResponseIsNotSuccess()
    {
        // arrange
        var requestUri = "https://api.somafm.com/channels.json";

        var statusCode = HttpStatusCode.NotFound;
        var httpClient = new HttpClient(TestHelper.CreateHttpMessageHandlerMock(HttpStatusCode.NotFound, requestUri, new StringContent(Guid.NewGuid().ToString())).Object);

        // act
        var result = await new SomaFm.SomaFmApiClient().RetrieveChannels(httpClient, CancellationToken.None);

        // assert
        Assert.False(result.Success);
        Assert.Null(result.Channels);
        Assert.Equal($"Server returned {(int)statusCode} {statusCode}", result.ErrorMessage);
    }

    private static Uri CreateUri()
    {
        return new Uri($"https://{Guid.NewGuid():N}.com/{Guid.NewGuid():N}");
    }
}
