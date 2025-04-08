using System.Net;
using System.Text.Json;
using Moss.ApiClient.SomaFm.Dto;

namespace Moss.ApiClient.SomaFm.Test.SomaFmApiClient;

public class RetrieveRecentlyPlayedSongsShould
{
    [Fact]
    public async Task RetrieveRecentlyPlayedSongsWhenAlbumIsProvided()
    {
        // arrange
        var channelId = Guid.NewGuid().ToString();

        var dto = new SongDto(
            Date: Convert.ToUInt32((DateTimeOffset.UtcNow - DateTimeOffset.UnixEpoch).TotalSeconds),
            Artist: Guid.NewGuid().ToString(),
            Album: Guid.NewGuid().ToString(),
            Title: Guid.NewGuid().ToString());

        // act
        var result = await ExerciseSut(channelId, dto);

        // assert
        Assert.NotNull(result);

        var song = result.Songs.Single();

        Assert.Equal(DateTimeOffset.UnixEpoch.AddSeconds(dto.Date), song.Timestamp);
        Assert.Equal(channelId, song.ChannelId);
        Assert.Equal(dto.Artist, song.Artist);
        Assert.Equal(dto.Album, song.Album);
        Assert.Equal(dto.Title, song.Title);
    }

    [Fact]
    public async Task RetrieveRecentlyPlayedSongsWhenAlbumIsNotProvided()
    {
        // arrange
        var channelId = Guid.NewGuid().ToString();

        var dto = new SongDto(
            Date: Convert.ToUInt32((DateTimeOffset.UtcNow - DateTimeOffset.UnixEpoch).TotalSeconds),
            Artist: Guid.NewGuid().ToString(),
            Album: string.Empty,
            Title: Guid.NewGuid().ToString());

        // act
        var result = await ExerciseSut(channelId, dto);

        // assert
        Assert.NotNull(result);

        var song = result.Songs.Single();

        Assert.Equal(DateTimeOffset.UnixEpoch.AddSeconds(dto.Date), song.Timestamp);
        Assert.Equal(channelId, song.ChannelId);
        Assert.Equal(dto.Artist, song.Artist);
        Assert.Null(song.Album);
        Assert.Equal(dto.Title, song.Title);
    }

    private async Task<RetrieveRecentlyPlayedSongsResponse> ExerciseSut(string channelId, SongDto dto)
    {
        var requestUri = $"https://api.somafm.com/songs/{channelId}.json";

        var songsDto = new SongsDto(new[] { dto });

        var httpClient = new HttpClient(TestHelper.CreateHttpMessageHandlerMock(HttpStatusCode.OK, requestUri, new StringContent(JsonSerializer.Serialize(songsDto))).Object);

        return await new SomaFm.SomaFmApiClient().RetrieveRecentlyPlayedSongs(httpClient, channelId, CancellationToken.None);
    }

    [Fact]
    public async Task ReturnFailureResponseWhenApiResponseIsNotSuccess()
    {
        // arrange
        var channelId = Guid.NewGuid().ToString();
        var requestUri = $"https://api.somafm.com/songs/{channelId}.json";

        var statusCode = HttpStatusCode.NotFound;
        var httpClient = new HttpClient(TestHelper.CreateHttpMessageHandlerMock(HttpStatusCode.NotFound, requestUri, new StringContent(Guid.NewGuid().ToString())).Object);

        // act
        var result = await new SomaFm.SomaFmApiClient().RetrieveRecentlyPlayedSongs(httpClient, channelId, CancellationToken.None);

        // assert
        Assert.False(result.Success);
        Assert.Null(result.Songs);
        Assert.Equal($"Server returned {(int)statusCode} {statusCode}", result.ErrorMessage);
    }
}
