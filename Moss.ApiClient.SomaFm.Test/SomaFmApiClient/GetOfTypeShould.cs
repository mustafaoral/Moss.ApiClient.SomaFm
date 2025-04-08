using System.IO.Compression;
using System.Net;
using System.Text.Json;
using static Moss.ApiClient.SomaFm.SomaFmApiClient;

namespace Moss.ApiClient.SomaFm.Test.SomaFmApiClient;

public class GetOfTypeShould
{
    [Fact]
    public async Task NotUseGzipWhenResponseContentEncodingDoesNotIndicateGzip()
    {
        // arrange
        var dto = CreateDto();

        // act
        var result = await ExerciseSut(new StreamContent(new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(dto))));

        // assert
        Assert.True(result.Success);
        Assert.Null(result.Message);
        Assert.Equal(dto.Id, result.Dto.Id);
        Assert.Equal(dto.Name, result.Dto.Name);
    }

    [Fact]
    public async Task UseGzipWhenResponseContentEncodingIndicatesGzip()
    {
        // arrange
        var dto = CreateDto();

        var inputStream = new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(dto));
        var outputStream = new MemoryStream();
        var compressionStream = new GZipStream(outputStream, CompressionLevel.NoCompression);

        try
        {
            inputStream.CopyTo(compressionStream);

            compressionStream.Flush();

            outputStream.Position = 0;


            var streamContent = new StreamContent(outputStream);

            streamContent.Headers.ContentEncoding.Add("gzip");

            // act
            var result = await ExerciseSut(streamContent);

            // assert
            Assert.True(result.Success);
            Assert.Null(result.Message);
            Assert.Equal(dto.Id, result.Dto.Id);
            Assert.Equal(dto.Name, result.Dto.Name);
        }
        finally
        {
            inputStream.Dispose();
        }
    }

    [Fact]
    public async Task ReturnFailureResponseWhenApiResponseIsNotSuccess()
    {
        // arrange
        var requestUri = $"https://{Guid.NewGuid():N}.com/{Guid.NewGuid():N}";
        var statusCode = HttpStatusCode.NotFound;

        var httpClient = new HttpClient(TestHelper.CreateHttpMessageHandlerMock(statusCode, requestUri, new StringContent(Guid.NewGuid().ToString())).Object);

        // act
        var result = await new SomaFm.SomaFmApiClient().Get<Dto>(httpClient, requestUri, CancellationToken.None);

        // assert
        Assert.False(result.Success);
        Assert.Null(result.Dto);
        Assert.Equal($"Server returned {(int)statusCode} {statusCode}", result.Message);
    }

    private static async Task<ApiResponse<Dto>> ExerciseSut(StreamContent streamContent)
    {
        var requestUri = $"https://{Guid.NewGuid():N}.com/{Guid.NewGuid():N}";

        var httpClient = new HttpClient(TestHelper.CreateHttpMessageHandlerMock(HttpStatusCode.OK, requestUri, streamContent).Object);

        return await new SomaFm.SomaFmApiClient().Get<Dto>(httpClient, requestUri, CancellationToken.None);
    }

    private static Dto CreateDto()
    {
        return new Dto
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString()
        };
    }

    private class Dto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
